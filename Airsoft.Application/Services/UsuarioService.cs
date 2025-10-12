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
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public UsuarioService(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
        }
        public async Task<ApiResponse<List<UsuarioResponse>>> GetUsuarioAll() {
            var lista = await _unitOfWork.UsuarioRepository.GetUsuariosAll();
            return new ApiResponse<List<UsuarioResponse>>
            {
                Success = true,
                Message = "Datos Obtenidos",
                Data = _mapper.Map<List<UsuarioResponse>>(lista),
            };
        }
        public async Task<ApiResponse<FindResponse<UsuarioResponse>>> GetUsuarioFind(FindRequest request)
        {
            var (usuarios, totalRegistros) = await _unitOfWork.UsuarioRepository.GetUsuarioFind(request.buscar,request.pagina, request.tamanoPagina);
            var paginacionResponse = new FindResponse<UsuarioResponse>
            {
                datos = _mapper.Map<List<UsuarioResponse>>(usuarios),
                pagina = request.pagina,
                tamanoPagina = request.tamanoPagina,
                totalRegistros = totalRegistros
            };
            return new ApiResponse<FindResponse<UsuarioResponse>>
            {
                Success = true,
                Message = "Datos Obtenidos",
                Data = paginacionResponse
            };
        }

        public async Task<ApiResponse<UsuarioResponse>> Create(UsuarioRequest request)
        {
            // para obtener los valores del token
            var aaaa = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);

            var valid = await _unitOfWork.UsuarioRepository.ExistsUsuario(request.UsuarioCuenta);

            if (valid)
                throw new ApiResponseExceptions(HttpStatusCode.Conflict, "El usuario ingresado existe");

            //var response = new ApiResponse<LoginResponse>();
            if(!request.Contrasena.Equals(request.ContrasenaConfirmar))
                throw new ApiResponseExceptions(HttpStatusCode.UnprocessableEntity, "Las contraseñas no son iguales");

            request.Contrasena = BCrypt.Net.BCrypt.HashPassword(request.Contrasena, workFactor: 12);

            var usuario = _mapper.Map<Usuario>(request);
            var Roles = await _unitOfWork.RolRepository.GetAllRol();

            if (!Roles.Any(x => x.RolID == request.RolID))
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "Rol no válido");
            //if (string.IsNullOrEmpty(request.RolNombre) || !Roles.Any(x => x.RolNombre.ToUpper() == request.RolNombre.ToUpper()))     
            //    throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "Rol no válido");

            usuario.RolID = Roles
                .First(x => x.RolID == request.RolID)
                .RolID;

            var res = await _unitOfWork.UsuarioRepository.SaveUsuario(usuario);

            if (!res)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "Existe un problema para registrar el usuario");

            return new ApiResponse<UsuarioResponse>()
            {
                Success = true,
                Message = "Login exitoso",
                Data = _mapper.Map<UsuarioResponse>(usuario),
            };
        }

        public async Task<ApiResponse<List<RolResponse>>> GetRol() {
            var Roles = await _unitOfWork.RolRepository.GetAllRol();

            return new ApiResponse<List<RolResponse>>()
            {
                Success = true,
                Message = "Roles obtenidos",
                Data = _mapper.Map<List<RolResponse>>(Roles),
            };
        }

        public async Task<ApiResponse<bool>> Update(UsuarioRequest request)
        {
            var usuarioID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            var roles = await _unitOfWork.RolRepository.GetAllRol();
            var valid = await _unitOfWork.UsuarioRepository.GetUsuariosByUsuarioID(request.UsuarioID);
            var existUsuario = await _unitOfWork.UsuarioRepository.ExistsUsuario(request.UsuarioCuenta);
            
            if(usuarioID==request.UsuarioID)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "El mismo usuario no puede actualizar sus datos");

            if (!valid.UsuarioCuenta.Equals(request.UsuarioCuenta) && existUsuario)
                throw new ApiResponseExceptions(HttpStatusCode.Conflict, "El usuario ingresado existe");

            if (!roles.FindAll(x => x.RolID == request.RolID).Any())
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "Rol no válido");

            //if (!request.Contrasena.Equals(request.ContrasenaConfirmar))
            //    throw new ApiResponseExceptions(HttpStatusCode.UnprocessableEntity, "Las contraseñas no son iguales");
            //request.Contrasena = BCrypt.Net.BCrypt.HashPassword(request.Contrasena, workFactor: 12);

            var usuario = _mapper.Map<Usuario>(request);

            var res = await _unitOfWork.UsuarioRepository.UpdateUsuario(usuario);

            return new ApiResponse<bool>()
            {
                Success = true,
                Message = "Se actualizo Correctamente el usuario",
                Data = res,
            };            

        }
    }
}
