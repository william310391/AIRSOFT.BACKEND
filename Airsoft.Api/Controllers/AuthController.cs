using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airsoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            var response = await _authService.Login(request);  
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("register")]
        [Authorize]
        [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsuarioResponse>> Registrar([FromBody] UsuarioRequest request) 
        {
            var response = await _authService.Registrar(request);
            return StatusCode(response.StatusCode, response);
        }



        //[HttpPost("login")]
        //public IActionResult Login([FromBody] LoginRequest request)
        //{
        //    // Aquí validarías usuario/contraseña en base de datos
        //    if (request.UsuarioNombre == "admin" && request.Password == "1234")
        //    {
        //        var token = _jwtService.GenerarToken(request.UsuarioNombre);
        //        return Ok(new { token });
        //    }
        //    return Unauthorized();
        //}
    }
}
