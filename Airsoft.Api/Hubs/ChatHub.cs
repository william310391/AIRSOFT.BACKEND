using Airsoft.Application.DTOs.Chat;
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
        public async Task JoinGroup(JoinGroupRequest request)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, request.chatID.ToString());
            await _service.UpdateUnread(request.chatID,request.usuarioID);

            await Clients.Group(request.chatID.ToString())
                .SendAsync("UserJoined", $" {request.userName} se unió al grupo {request.chatID}");
        }
        [Authorize()]
        public async Task SendMessageToGroup(MensajeSaveRequest request)
        {
            var result = await _service.Save(request);
            if (result?.Data != null)
            {
                await Clients.Group(request.chatID.ToString())
                    .SendAsync("ReceiveGroupMessage", result.Data);
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
