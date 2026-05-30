using Airsoft.Application.DTOs;
using Airsoft.Application.DTOs.Mensaje;
using Airsoft.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airsoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensajeController : ControllerBase
    {
        private readonly IMensajeService _service;

        public MensajeController(IMensajeService service)
        {
            _service = service;
        }

        [HttpPost("getMensaje")]
        [Authorize()]
        [ProducesResponseType(typeof(List<MensajeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<List<MensajeResponse>>>> GetMensaje([FromBody] GetMensajeRequest request)
        {
            var response = await _service.GetMensajesByChatID(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("updateUnread")]
        [Authorize()]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateUnread([FromBody] UpdateUnreadRequest request)
        {
            var response = await _service.UpdateUnread(request.chatID, request.usuarioID);
            return StatusCode(response.StatusCode, response);
        }

    }
}

