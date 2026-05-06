using Airsoft.Application.DTOs;
using Airsoft.Application.DTOs.ContactoSolicitud;
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

        [HttpPost("create")]
        [Authorize()]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<bool>>> Create([FromBody] ContactoSolicitudSaveRequest request)
        {
            var response = await _contactoSolicitudService.Create(request);
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

        [HttpGet("getSolicitudesPendientes")]
        [Authorize()]
        [ProducesResponseType(typeof(ApiResponse<List<GetSolicitudPendientesResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<List<GetSolicitudPendientesResponse>>>> getSolicitudesPendiente()
        {
            var response = await _contactoSolicitudService.GetSolicitudPendientes();
            return StatusCode(response.StatusCode, response);
        }


    }
}
