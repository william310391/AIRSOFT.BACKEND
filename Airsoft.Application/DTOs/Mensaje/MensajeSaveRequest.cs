namespace Airsoft.Application.DTOs.Mensaje
{
    public class MensajeSaveRequest
    {
        public Guid chatID { get; set; }
        public required string contenido { get; set; }
    }
}
