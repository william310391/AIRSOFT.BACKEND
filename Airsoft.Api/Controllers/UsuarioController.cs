using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airsoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMenuPaginaService _menuPaginaService;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IMenuPaginaService menuPaginaService, IUsuarioService usuarioService)
        {
            _menuPaginaService = menuPaginaService;
            _usuarioService = usuarioService;
        }

        [HttpPost("obtenerAccesos")]
        [Authorize()]
        [ProducesResponseType(typeof(ObtenerAccesosResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ObtenerAccesosResponse>> obtenerAccesos([FromBody] ObtenerAccesosRequest request)
        {
            var response = await _menuPaginaService.ObtenerAccesos(request);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("getUsuarioAll")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsuarioResponse>> GetUsuarioAll()
        {
            var response = await _usuarioService.GetUsuarioAll();
            return StatusCode(response.StatusCode, response);

        }

    }
}
