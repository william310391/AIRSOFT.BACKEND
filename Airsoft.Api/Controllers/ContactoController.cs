using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airsoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {
        private readonly IContactoService _contactoService;

        public ContactoController(IContactoService contactoService)
        {
            _contactoService = contactoService;
        }

        [HttpGet("getContactos")]
        [Authorize()]
        [ProducesResponseType(typeof(List<GetContactosByUsuarioIDResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<GetContactosByUsuarioIDResponse>>> GetContactos()
        {
            var response = await _contactoService.GetContactos();
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("findContactoByBuscar")]
        [Authorize()]
        [ProducesResponseType(typeof(List<FindContactoByBuscarResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<FindContactoByBuscarResponse>>> FindContactoByBuscar([FromBody] FindContactoByBuscarRequest req)
        {
            var response = await _contactoService.FindContactoByBuscar(req);
            return StatusCode(response.StatusCode, response);
        }



    }
}
