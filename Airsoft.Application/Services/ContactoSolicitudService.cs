using Airsoft.Application.DTOs.ContactoSolicitud;
using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Exceptions;
using Airsoft.Application.Interfaces;
using Airsoft.Domain.Entities;
using Airsoft.Domain.Enum;
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;
using AutoMapper;
using System.Net;

namespace Airsoft.Application.Services
{
    public class ContactoSolicitudService: IContactoSolicitudService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly DapperContext _dapperContext;
        public ContactoSolicitudService(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService,DapperContext dapperContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
            _dapperContext = dapperContext;
        }

        public async Task<ApiResponse<List<GetSolicitudPendientesResponse>>> GetSolicitudPendientes()
        {
            var usuarioID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            var lista = await _unitOfWork.ContactoSolicitudRepository.GetSolicitudPendientesByUsuarioID(usuarioID);

            return new ApiResponse<List<GetSolicitudPendientesResponse>>
            {
                Success= true,
                Message= lista.Any()? "Con solicitudes pendientes": "Sin solicitudes pendientes",
                Data = _mapper.Map<List<GetSolicitudPendientesResponse>>(lista),
            };
        }

        public async Task<ApiResponse<bool>> Create(ContactoSolicitudSaveRequest request)
        {
            var validarContacto = await _unitOfWork.UsuarioRepository.GetUsuariosByUsuarioID(request.usuarioContactoID);
            if (validarContacto == null || !validarContacto.Estado)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "El usuario de contacto no existe");

            var usuarioID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            var entidad = _mapper.Map<ContactoSolicitud>(request);
            entidad.ContactoSolicitudID = Guid.NewGuid();

            //Analua todas las solicitud envidas Enviadas anteriormente por el usuario
            await AnularSolicitudes(usuarioID, request.usuarioContactoID);

            entidad.UsuarioID = usuarioID;
            var res = await _unitOfWork.ContactoSolicitudRepository.Save(entidad);
            return new ApiResponse<bool>
            {
                Success = true,
                Message = "solicitud contacto enviada exitosamente",
                Data = res
            };
        }


        public async Task<ApiResponse<bool>> ChangeStatus(ContactoSolicitudChangeStatusRequest request)
        {

            //await using var tx = await _dapperContext.GetDapperTransaction();
            //var transaction = tx.Transaction;

            var (connection, transaction) = await _dapperContext.BeginTransactionAsync();
            await using (connection)
            await using (transaction)

                try
            {
                var entidad = _mapper.Map<ContactoSolicitud>(request);
                entidad.UsuarioID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
                var res = await _unitOfWork.ContactoSolicitudRepository.ChangeStatusByContactoSolicitudID(entidad, transaction);

                if (!res)
                {
                    throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "No se pudo actualizar el estado de la solicitud de contacto");
                }

                switch (entidad.EstadoID)
                {
                    case EnumEstados.SolicitudContacto.Aceptado: //receptor
                                                                 //registrar contacto en la tabla de contactos 
                        await _unitOfWork.ContactoRepository.Save(entidad.UsuarioID, entidad.UsuarioContactoID, transaction);
                        await _unitOfWork.ContactoRepository.Save(entidad.UsuarioContactoID, entidad.UsuarioID, transaction);

                        //crear sala de chat 
                        var chartID = Guid.NewGuid();
                        var result = await _unitOfWork.ChatRepository.Save(new Chat
                        {
                            ChatID = chartID,
                            NombreChat = $"Chat entre {entidad.UsuarioID} y {entidad.ContactoSolicitudID}",
                            EsPrivado = true,
                            UsuarioRegistroID = entidad.UsuarioID
                        }, transaction);

                        if (!result)
                        {
                            await transaction.RollbackAsync();
                            throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "No se pudo crear el chat");
                        }

                        //registrar usaurios sala de chat
                        await _unitOfWork.ChatMiembroRepository.Save(new ChatMiembro
                        {
                            ChatID = chartID,
                            UsuarioID = entidad.UsuarioID,
                            UsuarioRegistroID = entidad.UsuarioID,
                            EsAdmin = true
                        },transaction);
                        await _unitOfWork.ChatMiembroRepository.Save(new ChatMiembro
                        {
                            ChatID = chartID,
                            UsuarioID = entidad.UsuarioContactoID,
                            UsuarioRegistroID = entidad.UsuarioID,
                            EsAdmin = true,
                        },transaction);
                        break;
                }

                await transaction.CommitAsync();

                return new ApiResponse<bool>
                {
                    Success = true,
                    Message = "estado de solicitud de contacto actualizada exitosamente",
                    Data = res
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new ApiResponseExceptions(HttpStatusCode.InternalServerError, "Error al validar la solicitud de contacto", ex);
            }
           
        }
        private async Task<bool> AnularSolicitudes(int usuarioID, int usuarioContactoID)
        {
            bool success = false;
            success = await _unitOfWork.ContactoSolicitudRepository.ChangeStatusByUsuarioID(new ContactoSolicitud
            {
                UsuarioID = usuarioID,
                UsuarioContactoID = usuarioContactoID,
                EstadoID = EnumEstados.SolicitudContacto.Anulado
            });

            success = success && await _unitOfWork.ContactoSolicitudRepository.ChangeStatusByUsuarioID(new ContactoSolicitud
            {
                UsuarioID = usuarioContactoID,
                UsuarioContactoID = usuarioID,
                EstadoID = EnumEstados.SolicitudContacto.Anulado
            });


            return success;
        }
    }
}
