using Airsoft.Application.DTOs;
using Airsoft.Application.DTOs.Chat;
using Airsoft.Application.DTOs.Contacto;
using Airsoft.Application.DTOs.ContactoSolicitud;
using Airsoft.Application.Interfaces;
using Airsoft.Domain.Entities;
using Airsoft.Domain.Enum;
using Airsoft.Infrastructure.Intefaces;
using AutoMapper;

namespace Airsoft.Application.Services
{
    public class ContactoService : IContactoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public ContactoService(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
        }
        public async Task<ApiResponse<List<ContatoDetalleResponse>>> GetContactos()
        {
            var usuarioID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            //if (usuarioID == 0)
            //    throw new ApiResponseExceptions(HttpStatusCode.Unauthorized, "Usuario no valido");

            //var usuario = _unitOfWork.UsuarioRepository.GetUsuariosByUsuarioID(usuarioID);
            //if (usuario == null)
            //    throw new ApiResponseExceptions(HttpStatusCode.Conflict, "El usuario ingresado existe");

            var data = await _unitOfWork.ContactoRepository.GetContactosByUsuarioID(usuarioID);
            var dataDTO = await CargarContactoDetalle(data);

            return new ApiResponse<List<ContatoDetalleResponse>>
            {
                Success = true,
                Message = dataDTO.Any() ? "hay datos" : "sin datos",
                Data = dataDTO,
            };
        }

        public async Task<ApiResponse<List<ContatoDetalleResponse>>> FindContactoByBuscar(FindContactoByBuscarRequest req)
        {
            var usuarioID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            //if (usuarioID == 0)
            //    throw new ApiResponseExceptions(HttpStatusCode.Unauthorized, "Usuario no valido");
            //var usuario = _unitOfWork.UsuarioRepository.GetUsuariosByUsuarioID(usuarioID);
            //if (usuario == null)
            //    throw new ApiResponseExceptions(HttpStatusCode.Conflict, "El usuario ingresado existe");

            if(req.buscar.Length < 3)
                return new ApiResponse<List<ContatoDetalleResponse>>
                {
                    Success = true,
                    Message = "La cadena de búsqueda debe tener al menos 3 caracteres",
                    Data = new List<ContatoDetalleResponse>(),
                };

            var data = await _unitOfWork.ContactoRepository.FindContactoByBuscar(usuarioID, req.buscar);
            var dataDTO = await CargarContactoDetalle(data);
            //res.ForEach(x => x.noContacto = data.Find(y => y.UsuarioID == usuarioID)?.UsuarioContactoID == null ? true : false);

            return new ApiResponse<List<ContatoDetalleResponse>>
            {
                Success = true,
                Message = dataDTO.Any() ? "hay datos" : "sin datos",
                Data = dataDTO,
            };
        }


        private async Task<List<ContatoDetalleResponse>> CargarContactoDetalle(List<Contacto> data)
        {
            if (data == null || !data.Any())
                return new List<ContatoDetalleResponse>();

            var dataDTO = _mapper.Map<List<ContatoDetalleResponse>>(data);

            // Solicitudes pendientes
            var solicitudesPorUsuario = data
                .Where(x => x.NoContacto)
                .GroupBy(x => x.UsuarioID)
                .ToDictionary(g => g.Key, g => g.First());

            // Chats disponibles
            var chatsPorUsuario = data
                .Where(x => x.ChatID.HasValue && x.ChatID != Guid.Empty)
                .GroupBy(x => x.UsuarioID)
                .ToDictionary(g => g.Key, g => g.First());

            foreach (var item in dataDTO)
            {
                // Datos solicitud pendiente
                if (solicitudesPorUsuario.TryGetValue(item.usuarioID, out var solicitud)
                    && solicitud.SolicitudPendiente)
                {
                    item.datosSolicitudPendiente = new DatoSolicitudPendiente
                    {
                        SolicitudPendiente = solicitud.SolicitudPendiente,
                        EsRemitente = solicitud.EsRemitente,
                        contactoSolicitudID = solicitud.ContactoSolicitudID,
                        solicitudUsuarioID = solicitud.SolicitudUsuarioID,
                        solicitudUsuarioContactoID = solicitud.SolicitudUsuarioContactoID,
                        solicitudMensaje = solicitud.SolicitudMensaje,
                    };
                }
                else
                {
                    item.datosSolicitudPendiente = null;
                }

                // Datos chat
                if (chatsPorUsuario.TryGetValue(item.usuarioID, out var chat))
                {
                    item.datosChat = new ChatResponse
                    {
                        NombreChat = chat.NombreChat ?? string.Empty,
                        ChatID = chat.ChatID ?? Guid.Empty,
                        EsPrivado = chat.EsPrivado,
                        MensajeNoleidos = chat.MensajeNoleidos,
                        UsuarioID = chat.UsuarioID,
                    };
                }
                else
                {
                    item.datosChat = null;
                }
            }

            return dataDTO;
        }
    }
}
