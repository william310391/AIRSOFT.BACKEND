
using Airsoft.Application.Interfaces;
using Airsoft.Domain.Entities;
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

            if(usuario==null || string.IsNullOrEmpty(usuario.RolNombre))
                throw new InvalidOperationException("rol no valido");

            var key = Encoding.UTF8.GetBytes(keyString.Trim());

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.UsuarioNombre),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, usuario.RolNombre),
            new Claim("UsuarioID",usuario.UsuarioID.ToString())
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
