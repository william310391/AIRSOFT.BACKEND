
using Airsoft.Application.Interfaces;
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

        public string GenerarToken(string usuarioId, string rol)
        {
            var jwtSettings = _config.GetSection("Jwt");

            var keyString = jwtSettings["Key"];
            if (string.IsNullOrEmpty(keyString))
                throw new InvalidOperationException("La clave JWT no está configurada.");

            var expiresStr = jwtSettings["ExpiresInMinutes"];
            if (string.IsNullOrWhiteSpace(expiresStr))
                throw new InvalidOperationException("La configuración 'ExpiresInMinutes' no está definida.");


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, usuarioId),
            new Claim(ClaimTypes.Role, rol),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(expiresStr)),
                signingCredentials: credenciales
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public string GenerarToken(string usuario)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var keyString = jwtSettings["Key"];
            if (string.IsNullOrEmpty(keyString))
            {
                throw new InvalidOperationException("La clave JWT no está configurada correctamente en la configuración.");
            }
            var key = Encoding.UTF8.GetBytes(keyString.Trim());

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, usuario),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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
    }
}
