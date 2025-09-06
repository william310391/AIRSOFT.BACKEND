
using Airsoft.Application.Interfaces;
using Airsoft.Domain.Entities;
using Airsoft.Domain.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Airsoft.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }
        public string GenerarToken(Usuario usuario)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var keyString = jwtSettings["Key"];
            if (string.IsNullOrEmpty(keyString))
                throw new InvalidOperationException("La clave JWT no está configurada correctamente en la configuración.");

            if (usuario == null || string.IsNullOrEmpty(usuario.RolNombre))
                throw new InvalidOperationException("rol no valido");

            var key = Encoding.UTF8.GetBytes(keyString.Trim());

            var claims = new List<Claim>
            {
                new Claim(EnumClaims.UsuarioNombre, usuario.UsuarioNombre),
                new Claim(EnumClaims.UsuarioID,usuario.UsuarioID.ToString()),
                new Claim(EnumClaims.UsuarioRol,usuario.RolNombre),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var credenciales = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            );


            var utcAhora = DateTime.UtcNow;
            var zonaPeru = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            var horaPeru = TimeZoneInfo.ConvertTimeFromUtc(utcAhora, zonaPeru);

            var fechaHoraUtc = DateTime.UtcNow.ToLocalTime();
            var zonaHoraria = horaPeru;
            Console.WriteLine(fechaHoraUtc); // Ej: 12/08/2025 14:32:15

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: fechaHoraUtc.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresInMinutes"])),
                signingCredentials: credenciales
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<JwtSecurityToken> ValidateTokenManualAsync(string token)
        {
            return await Task.Run(() =>
            {
                return ValidateTokenManual(token);
            });
        }
        public JwtSecurityToken ValidateTokenManual(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSettings = _config.GetSection("Jwt");
            var keyString = jwtSettings["Key"];

            if (string.IsNullOrEmpty(keyString))
                throw new InvalidOperationException("La clave JWT no está configurada correctamente en la configuración.");

            var key = Encoding.UTF8.GetBytes(keyString.Trim());
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            return jwtToken;
        }

        public IEnumerable<Claim> ObtenerClaims(JwtSecurityToken jwtSecurityToken)
        {
            return jwtSecurityToken.Claims;
        }
    }
}
