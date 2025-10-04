using Airsoft.Application.Interfaces;
using Airsoft.Domain.Enum;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace Airsoft.Application.Services
{
    public class UserContextService: IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

        public string? GetUsuarioID() => User?.FindFirst(EnumClaims.UsuarioID)?.Value;
        public string? GetUsuarioCuenta() => User?.FindFirst(EnumClaims.UsuarioCuenta)?.Value;
        public string? GetRol() => User?.FindFirst(EnumClaims.UsuarioRol)?.Value;
        //public string? GetAttribute(string attribute) => User?.FindFirst(attribute)?.Value;
        public T? GetAttribute<T>(string attribute)
        {
            var value = User?.FindFirst(attribute)?.Value;

            if (string.IsNullOrEmpty(value))
                return default;

            return JsonSerializer.Deserialize<T>(value);
        }
    }
}
