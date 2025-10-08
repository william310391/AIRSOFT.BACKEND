using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Interfaces;
using Airsoft.Application.Services;
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

        [HttpPost("getUsuarioFind")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(FindResponse<UsuarioResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FindResponse<UsuarioResponse>>> GetUsuarioFind([FromBody] FindRequest request)
        {
            var response = await _usuarioService.GetUsuarioFind(request);
            return StatusCode(response.StatusCode, response);

        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsuarioResponse>> Create([FromBody] UsuarioRequest request)
        {
            var response = await _usuarioService.Create(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("getRol")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(RolResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RolResponse>> GetRol()
        {
            var response = await _usuarioService.GetRol();
            return StatusCode(response.StatusCode, response);
        }

    }
}
