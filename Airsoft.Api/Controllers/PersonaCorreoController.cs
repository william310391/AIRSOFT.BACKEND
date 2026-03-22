using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Interfaces;
using Airsoft.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airsoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaCorreoController : ControllerBase
    {
        private readonly IPersonaCorreoServices _personaCorreoServices;

        public PersonaCorreoController(IPersonaCorreoServices personaCorreoServices) {
            _personaCorreoServices = personaCorreoServices;
        }

        [HttpGet("getPersonaCorreos")]
        [Authorize()]
        [ProducesResponseType(typeof(List<PersonaCorreo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<PersonaCorreo>>> GetPersonaCorreos([FromQuery] int personaID)
        {
            var response = await _personaCorreoServices.GetByPersonaID(personaID);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("save")]
        [Authorize()]
        [ProducesResponseType(typeof(List<PersonaCorreo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<PersonaCorreo>>> Save([FromBody] PersonaCorreoRequest request)
        {
            var response = await _personaCorreoServices.Save(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("update")]
        [Authorize()]
        [ProducesResponseType(typeof(List<PersonaCorreo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<PersonaCorreo>>> update([FromBody] PersonaCorreoRequest request)
        {
            var response = await _personaCorreoServices.Update(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("changeState")]
        [Authorize()]
        [ProducesResponseType(typeof(List<PersonaCorreo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<bool>>> ChangeState([FromBody] PersonaCorreoRequest request)
        {
            var response = await _personaCorreoServices.ChangeState(request);
            return StatusCode(response.StatusCode, response);
        }


    }
}
