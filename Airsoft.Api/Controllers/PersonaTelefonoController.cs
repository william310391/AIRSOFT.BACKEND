using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Interfaces;
using Airsoft.Application.Services;
using Airsoft.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airsoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaTelefonoController : ControllerBase
    {
        private readonly IPersonaTelefonoService _personaTelefonoService;

        public PersonaTelefonoController(IPersonaTelefonoService personaTelefonoService)
        {
            _personaTelefonoService = personaTelefonoService;
        }

        [HttpGet("getPersonaTelefonos")]
        [Authorize()]
        [ProducesResponseType(typeof(List<PersonaTelefonoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<PersonaCorreoResponse>>> GetPersonaTelefonos([FromQuery] int personaID)
        {
            var response = await _personaTelefonoService.GetByPersonaID(personaID);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("save")]
        [Authorize()]
        [ProducesResponseType(typeof(List<PersonaTelefonoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<PersonaTelefonoResponse>>> Save([FromBody] PersonaTelefonoRequest request)
        {
            var response = await _personaTelefonoService.Save(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("update")]
        [Authorize()]
        [ProducesResponseType(typeof(List<PersonaTelefonoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<PersonaTelefonoResponse>>> update([FromBody] PersonaTelefonoRequest request)
        {
            var response = await _personaTelefonoService.Update(request);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPut("changeState")]
        [Authorize()]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<bool>>> ChangeState([FromBody] PersonaTelefonoRequest request)
        {
            var response = await _personaTelefonoService.ChangeState(request);
            return StatusCode(response.StatusCode, response);
        }



    }
}
