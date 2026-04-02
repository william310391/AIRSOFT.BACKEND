using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airsoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly IPaisService _paisService;
        public PaisController(IPaisService paisService) => _paisService = paisService;

        [HttpGet("getPais")]
        [Authorize]
        [ProducesResponseType(typeof(List<PaisResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PaisResponse>>> GetPaisAll()
        {
            var response = await _paisService.GetPaisAll();
            return StatusCode(response.StatusCode, response);
        }
    }
}
