using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airsoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoSolicitudController : ControllerBase
    {
        private readonly IContactoSolicitudService _contactoSolicitudService;
        public ContactoSolicitudController(IContactoSolicitudService contactoSolicitudService)
        {
            _contactoSolicitudService = contactoSolicitudService;
        }

        [HttpPost("save")]
        [Authorize()]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<bool>>> Save([FromBody] ContactoSolicitudSaveRequest request)
        {
            var response = await _contactoSolicitudService.Save(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("changeStatus")]
        [Authorize()]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<bool>>> ChangeStatus([FromBody] ContactoSolicitudChangeStatusRequest request)
        {
            var response = await _contactoSolicitudService.ChangeStatus(request);
            return StatusCode(response.StatusCode, response);
        }

    }
}
