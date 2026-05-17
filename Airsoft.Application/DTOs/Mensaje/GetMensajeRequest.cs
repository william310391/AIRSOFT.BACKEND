namespace Airsoft.Application.DTOs.Mensaje
{
    public class GetMensajeRequest
    {
        public Guid chatID { get; set; }
        public int pageSize { get; set; } = 100;
    }
}
