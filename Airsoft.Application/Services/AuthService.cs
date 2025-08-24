using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Exceptions;
using Airsoft.Application.Interfaces;
using Airsoft.Domain.Entities;
using Airsoft.Infrastructure.Intefaces;
using AutoMapper;
using System.Net;
namespace Airsoft.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IUserContextService _userContextService;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IJwtService jwtService, IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jwtService;
            _userContextService = userContextService;
        }        
        public async Task<ApiResponse<LoginResponse>> Login(LoginRequest request)
        {
            //var response = new ApiResponse<LoginResponse>();

            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(request.UsuarioNombre) || string.IsNullOrWhiteSpace(request.Password))
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "Debe ingresar usuario y contraseña");

            // Buscar usuario
            var entidad = await _unitOfWork.UsuarioRepository.GetUsuariosByUsuarioNombre(request.UsuarioNombre!);

            if (entidad == null || string.IsNullOrEmpty(entidad.UsuarioNombre) || string.IsNullOrEmpty(entidad.Contrasena))
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "El usuario invalido");             
       
            // Verificar contraseña sin volver a hashear
            if (!BCrypt.Net.BCrypt.Verify(request.Password, entidad.Contrasena))
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "Contraseña incorrecta");    

            // Si todo OK, generar token
            var loginResponse = new LoginResponse
            {
                UsuarioId = entidad.UsuarioID,
                NombreUsuario = entidad.UsuarioNombre,
                Token = _jwtService.GenerarToken(entidad)
            };

            return new ApiResponse<LoginResponse>() { 
                Success= true,
                Message = "Login exitoso",
                Data = loginResponse
            };
        }

        public async Task<ApiResponse<UsuarioResponse>> Registrar(UsuarioRequest request)
        {
            // para obtener los valores del token
            var aaaa = _userContextService.GetAttribute<int>("UsuarioID");

            var valid = await _unitOfWork.UsuarioRepository.ExistsUsuario(request.UsuarioNombre);

            if (valid)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "El usuario ingresado existe");

            var response = new ApiResponse<LoginResponse>();
            request.Contrasena = BCrypt.Net.BCrypt.HashPassword(request.Contrasena, workFactor: 12);

            var usuario = _mapper.Map<Usuario>(request);            
            var Roles = await _unitOfWork.RolRepository.GetAllRol();

            if (string.IsNullOrEmpty(request.RolNombre) || !Roles.Any(x => x.RolNombre.ToUpper() == request.RolNombre.ToUpper()))     
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "Rol no válido");
      
            usuario.RolID = Roles
                .First(x => x.RolNombre.ToUpper() == request.RolNombre.ToUpper())
                .RolID;

            var res = await _unitOfWork.UsuarioRepository.SaveUsuario(usuario);

            if (!res)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "Existe un problema para registrar el usuario");

            return new ApiResponse<UsuarioResponse>()
            {
                Success = true,
                Message = "Login exitoso",
                Data= _mapper.Map<UsuarioResponse>(usuario),                
            };
        }

    }
}
