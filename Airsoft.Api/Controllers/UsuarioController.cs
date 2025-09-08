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

        public UsuarioController(IMenuPaginaService menuPaginaService) {
            _menuPaginaService = menuPaginaService;
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

    }
}
