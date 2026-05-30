namespace Airsoft.Application.DTOs.Mensaje
{
    public class UpdateUnreadRequest
    {
        public Guid chatID { get; set; }
        public int usuarioID { get; set; }
    }
}
