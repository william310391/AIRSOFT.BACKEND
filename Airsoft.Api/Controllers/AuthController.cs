using Airsoft.Application.DTOs.Request;
using Airsoft.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Airsoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Aquí validarías usuario/contraseña en base de datos
            if (request.UsuarioNombre == "admin" && request.Password == "1234")
            {
                var token = _jwtService.GenerarToken(request.UsuarioNombre);
                return Ok(new { token });
            }
            return Unauthorized();
        }
    }
}
