using Airsoft.Application.DTOs;
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
            var dataDTO = _mapper.Map<List<ContatoDetalleResponse>>(data);

            var solicitudesPorUsuario = data
                .Where(x => x.NoContacto) // o x.ContactoSolicitudID != null según tipo
                .ToDictionary(x => x.UsuarioID, x => x);

            if (!dataDTO.Any())
            {
                new List<ContatoDetalleResponse>();
            }

            dataDTO.ForEach(y =>
            {
                if (solicitudesPorUsuario.TryGetValue(y.usuarioID, out var x)
                    && x.NoContacto && x.SolicitudPendiente  // o != Guid.Empty según tipo
                    )
                {
                    y.datosSolicitudPendiente = new DatoSolicitudPendiente
                    {
                        SolicitudPendiente = x.SolicitudPendiente,
                        EsRemitente = x.EsRemitente,
                        contactoSolicitudID = x.ContactoSolicitudID,
                        solicitudUsuarioID = x.SolicitudUsuarioID,
                        solicitudUsuarioContactoID = x.SolicitudUsuarioContactoID,
                        solicitudMensaje = x.SolicitudMensaje,
                    };
                }
                else
                {
                    y.datosSolicitudPendiente = null;
                }
            });
            return dataDTO;
        }
    }
}
