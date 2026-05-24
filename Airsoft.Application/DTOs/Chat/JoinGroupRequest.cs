namespace Airsoft.Application.DTOs.Chat
{
    public class JoinGroupRequest
    {
        public Guid chatID { get; set; }
        public int usuarioID { get; set; }
        public required string userName { get; set; }
    }
}
