using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airsoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaService _personaService;
        public PersonaController(IPersonaService personaService) => _personaService = personaService;

        /// <summary>
        /// Obtiene la lista de personas.
        /// </summary>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<PersonaResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PersonaResponse>>> GetPersonas()
        {
            var response = await _personaService.GetPersonas();
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Obtiene persona por id.
        /// </summary>
        [HttpGet("{personaID:int}")]
        [Authorize]
        [ProducesResponseType(typeof(PersonaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PersonaResponse>> GetPersonaByID(int personaID)
        {
            var response = await _personaService.GetPersonaByID(personaID);

            if (response == null)
                return NotFound();

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("save")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> Save([FromBody] PersonaRequest request)
        {
            var response = await _personaService.Save(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("update")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> Update([FromBody] PersonaRequest request)
        {
            var response = await _personaService.Update(request);
            return StatusCode(response.StatusCode, response);
        }
    }
}
