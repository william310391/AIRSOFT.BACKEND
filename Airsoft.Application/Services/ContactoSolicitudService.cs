using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Exceptions;
using Airsoft.Application.Interfaces;
using Airsoft.Domain.Entities;
using Airsoft.Domain.Enum;
using Airsoft.Infrastructure.Intefaces;
using AutoMapper;
using System.Net;

namespace Airsoft.Application.Services
{
    public class ContactoSolicitudService: IContactoSolicitudService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        public ContactoSolicitudService(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<ApiResponse<bool>> Save(ContactoSolicitudSaveRequest request)
        {
            var validarContacto = await _unitOfWork.UsuarioRepository.GetUsuariosByUsuarioID(request.UsuarioContactoID);
            if (validarContacto == null || !validarContacto.Estado)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "El usuario de contacto no existe");

            var usuarioID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            var entidad = _mapper.Map<ContactoSolicitud>(request);

            //Analua todas las solicitud envidas Enviadas anteriormente por el usuario
            await AnularSolicitudes(usuarioID, request.UsuarioContactoID);

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


            try
            {
                var entidad = _mapper.Map<ContactoSolicitud>(request);
                entidad.UsuarioID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
                var res = await _unitOfWork.ContactoSolicitudRepository.ChangeStatus(entidad);

                if (!res)
                    throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "No se pudo actualizar el estado de la solicitud de contacto");

                switch (entidad.EstadoID)
                {
                    case EnumEstados.SolicitudContacto.Aceptado: //receptor
                                                                 //registrar contacto en la tabla de contactos 
                        await _unitOfWork.ContactoRepository.Save(entidad.UsuarioID, entidad.ContactoSolicitudID);
                        await _unitOfWork.ContactoRepository.Save(entidad.ContactoSolicitudID, entidad.UsuarioID);

                        //crear sala de chat 
                        var chartID = Guid.NewGuid();
                        await _unitOfWork.ChatRepository.Save(new Chat
                        {
                            ChatID = chartID,
                            NombreChat = $"Chat entre {entidad.UsuarioID} y {entidad.ContactoSolicitudID}",
                            EsPrivado = true,
                            UsuarioRegistroID = entidad.UsuarioID
                        });

                        //registrar usaurios sala de chat
                        await _unitOfWork.ChatMiembroRepository.Save(new ChatMiembro
                        {
                            ChatID = chartID,
                            UsuarioID = entidad.UsuarioID,
                            UsuarioRegistroID = entidad.UsuarioID,
                            EsAdmin = true
                        });
                        await _unitOfWork.ChatMiembroRepository.Save(new ChatMiembro
                        {
                            ChatID = chartID,
                            UsuarioID = entidad.ContactoSolicitudID,
                            UsuarioRegistroID = entidad.UsuarioID,
                            EsAdmin = true,
                        });
                        break;
                }

                return new ApiResponse<bool>
                {
                    Success = true,
                    Message = "estado de solicitud de contacto actualizada exitosamente",
                    Data = res
                };
            }
            catch (Exception ex)
            {
                throw new ApiResponseExceptions(HttpStatusCode.InternalServerError, "Error al validar la solicitud de contacto", ex);
            }
           
        }


        private async Task<bool> AnularSolicitudes(int usuarioID, int usuarioContactoID)
        {
            bool success = false;
            success = await _unitOfWork.ContactoSolicitudRepository.ChangeStatus(new ContactoSolicitud
            {
                UsuarioID = usuarioID,
                ContactoSolicitudID = usuarioContactoID,
                EstadoID = EnumEstados.SolicitudContacto.Anulado
            });

            success = success && await _unitOfWork.ContactoSolicitudRepository.ChangeStatus(new ContactoSolicitud
            {
                UsuarioID = usuarioContactoID,
                ContactoSolicitudID = usuarioID,
                EstadoID = EnumEstados.SolicitudContacto.Anulado
            });


            return success;
        }
    }
}
