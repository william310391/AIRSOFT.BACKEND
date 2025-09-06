using Airsoft.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Airsoft.Application.Interfaces
{
    public interface IJwtService
    {   
        string GenerarToken(Usuario usuario);
        JwtSecurityToken ValidateTokenManual(string token);
        Task<JwtSecurityToken> ValidateTokenManualAsync(string token);
        IEnumerable<Claim> ObtenerClaims(JwtSecurityToken jwtSecurityToken);
    }
}
