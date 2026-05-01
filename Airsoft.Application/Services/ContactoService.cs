using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Interfaces;
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
        public async Task<ApiResponse<List<GetContactosByUsuarioIDResponse>>> GetContactos()
        {
            var usuarioID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            //if (usuarioID == 0)
            //    throw new ApiResponseExceptions(HttpStatusCode.Unauthorized, "Usuario no valido");

            //var usuario = _unitOfWork.UsuarioRepository.GetUsuariosByUsuarioID(usuarioID);
            //if (usuario == null)
            //    throw new ApiResponseExceptions(HttpStatusCode.Conflict, "El usuario ingresado existe");

            var data = await _unitOfWork.ContactoRepository.GetContactosByUsuarioID(usuarioID);

            return new ApiResponse<List<GetContactosByUsuarioIDResponse>>
            {
                Success = true,
                Message = data.Any() ? "hay datos" : "sin datos",
                Data = _mapper.Map<List<GetContactosByUsuarioIDResponse>>(data),
            };
        }

        public async Task<ApiResponse<List<FindContactoByBuscarResponse>>> FindContactoByBuscar(FindContactoByBuscarRequest req)
        {
            var usuarioID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            //if (usuarioID == 0)
            //    throw new ApiResponseExceptions(HttpStatusCode.Unauthorized, "Usuario no valido");
            //var usuario = _unitOfWork.UsuarioRepository.GetUsuariosByUsuarioID(usuarioID);
            //if (usuario == null)
            //    throw new ApiResponseExceptions(HttpStatusCode.Conflict, "El usuario ingresado existe");

            if(req.buscar.Length < 3)
                return new ApiResponse<List<FindContactoByBuscarResponse>>
                {
                    Success = true,
                    Message = "La cadena de búsqueda debe tener al menos 3 caracteres",
                    Data = new List<FindContactoByBuscarResponse>(),
                };

            var data = await _unitOfWork.ContactoRepository.FindContactoByBuscar(usuarioID, req.buscar);
            var res = _mapper.Map<List<FindContactoByBuscarResponse>>(data);
            res.ForEach(x => x.noContacto = data.Find(y => y.UsuarioID == usuarioID)?.UsuarioContactoID == null ? true : false);

            return new ApiResponse<List<FindContactoByBuscarResponse>>
            {
                Success = true,
                Message = data.Any() ? "hay datos" : "sin datos",
                Data = res,
            };
        }
    }
}
