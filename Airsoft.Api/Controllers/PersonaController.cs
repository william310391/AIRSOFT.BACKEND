using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Interfaces;
using Azure;
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
        [ProducesResponseType(typeof(PersonaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PersonaResponse>> GetPersonaByID(int personaID)
        {
            var response = await _personaService.GetPersonaByID(personaID);

            if (response == null)
                return NotFound();

            return StatusCode(response.StatusCode, response);
        }
    }
}
