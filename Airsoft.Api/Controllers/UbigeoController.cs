using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airsoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbigeoController : ControllerBase
    {
        private readonly IUbigeoService _ubigeoService;
        public UbigeoController(IUbigeoService ubigeoService)
        {
            _ubigeoService = ubigeoService;
        }

        [HttpGet("getDepartamentos")]
        [Authorize]
        [ProducesResponseType(typeof(List<UbigeoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<UbigeoResponse>>> GetUbigeoByPersonaID()
        {
            var response = await _ubigeoService.GetDepartamentos();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("getProvincias/{departamentoID:int}")]
        [Authorize]
        [ProducesResponseType(typeof(List<UbigeoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<UbigeoResponse>>> GetProvincias(int departamentoID)
        {
            var response = await _ubigeoService.GetProvincias(departamentoID);
            return StatusCode(response.StatusCode, response);
        }


        [HttpGet("getDistritos/{departamentoID:int}/{provinciaID:int}")]
        [Authorize]
        [ProducesResponseType(typeof(List<UbigeoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<UbigeoResponse>>> GetDistritos(int departamentoID,int provinciaID)
        {
            var response = await _ubigeoService.GetDistritos(departamentoID,provinciaID);
            return StatusCode(response.StatusCode, response);
        }
    }
}
