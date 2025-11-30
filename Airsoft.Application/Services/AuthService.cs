using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Exceptions;
using Airsoft.Application.Interfaces;
using Airsoft.Domain.Entities;
using Airsoft.Domain.Enum;
using Airsoft.Infrastructure.Intefaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Net;
namespace Airsoft.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IJwtService jwtService, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
        }        
        public async Task<ApiResponse<LoginResponse>> Login(LoginRequest request)
        {
            //var response = new ApiResponse<LoginResponse>();

            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(request.UsuarioCuenta) || string.IsNullOrWhiteSpace(request.Password))
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "Debe ingresar usuario y contraseña");

            // Buscar usuario
            var entidad = await _unitOfWork.UsuarioRepository.GetUsuarioByUsuarioCuenta(request.UsuarioCuenta!);

            if (entidad == null || string.IsNullOrEmpty(entidad.UsuarioCuenta) || string.IsNullOrEmpty(entidad.Contrasena))
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "El usuario invalido");             
       
            // Verificar contraseña sin volver a hashear
            if (!BCrypt.Net.BCrypt.Verify(request.Password, entidad.Contrasena))
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "Contraseña incorrecta");

            var lista = await _unitOfWork.MenuPaginaRepository.GetMenuPaginasByPersonaID(entidad.UsuarioID,entidad.RolID);
            var listaDTO = _mapper.Map<List<MenuPaginaResponse>>(lista);


            // Si todo OK, generar token
            var loginResponse = new LoginResponse
            {
                UsuarioId = entidad.UsuarioID,
                UsuarioCuenta = entidad.UsuarioCuenta,
                UsuarioNombre = entidad.UsuarioNombre,
                Token = _jwtService.GenerarToken(entidad)
            };

            return new ApiResponse<LoginResponse>() { 
                Success= true,
                Message = "Login exitoso",
                Data = loginResponse
            };
        }
        public async Task<ApiResponse<ValidarTokenResponse>> ValidarToken()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null || httpContext.User?.Identity == null || !httpContext.User.Identity.IsAuthenticated)
            {
                throw new ApiResponseExceptions(HttpStatusCode.Unauthorized, "No se encontró un usuario autenticado.");     
            }

            var response = new ApiResponse<ValidarTokenResponse>
            {
                Success = true,
                Message = "Token válido",
                Data = new ValidarTokenResponse
                {
                    isTokenValido = true,
                }             
            };

           return await Task.FromResult(response);
        }

    }
}
