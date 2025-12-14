using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airsoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatosController : ControllerBase
    {
        private readonly IDatosService _datosService;
        public DatosController(IDatosService datosService)
        {
            _datosService = datosService;
        }

        [HttpPost("findBuscarDato")]
        [Authorize()]
        [ProducesResponseType(typeof(FindResponse<DatosResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FindResponse<DatosResponse>>> FindBuscarDato([FromBody] FindRequest request)
        {
            var response = await _datosService.FindBuscarDato(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("findByTipoDato")]
        [Authorize()]
        [ProducesResponseType(typeof(List<DatosResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<DatosResponse>>> FindByTipoDato([FromQuery] string tipoDato)
        {
            var response = await _datosService.FindByTipoDato(tipoDato);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<DatosResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<DatosResponse>>> Create([FromBody] DatosRequest request)
        {
            var response = await _datosService.Create(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<DatosResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<DatosResponse>>> Update([FromBody] DatosRequest request)
        {
            var response = await _datosService.Update(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("changeState")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<DatosResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<DatosResponse>>> ChangeState([FromBody] DatosChangeStateRequest request)
        {
            var response = await _datosService.ChangeState(request);
            return StatusCode(response.StatusCode, response);
        }

    }
}
