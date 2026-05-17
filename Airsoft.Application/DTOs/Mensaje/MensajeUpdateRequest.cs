namespace Airsoft.Application.DTOs.Mensaje
{
    public class MensajeUpdateRequest
    {
        public Guid chatID { get; set; }
        public Guid mensajeID { get; set; }
        public required string contenido { get; set; }

    }
}
