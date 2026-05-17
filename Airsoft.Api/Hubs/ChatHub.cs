using Airsoft.Application.DTOs.Mensaje;
using Airsoft.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
namespace Airsoft.Api.Hubs
{  
    public class ChatHub: Hub
    {
        private readonly IMensajeService _service;

        public ChatHub(IMensajeService service)
        {
            _service = service;
        }


        [Authorize()]
        public async Task JoinGroup(string groupName, string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName)
                .SendAsync("UserJoined", $"{userName} se unió al grupo {groupName}");
        }
        [Authorize()]
        public async Task SendMessageToGroup(MensajeSaveRequest request)
        {
            try
            {
                var result = await _service.Save(request);
                await Clients.Group(request.chatID.ToString()).SendAsync("ReceiveGroupMessage", result.Data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize()]
        public async Task LeaveGroup(string groupName, string userName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName)
                .SendAsync("UserLeft", $"{userName} salió del grupo {groupName}");
        }
    }
}
