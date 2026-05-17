namespace Airsoft.Application.DTOs.Mensaje
{
    public class MensajeRequest
    {
        public Guid chatID { get; set; }
        public Guid mensajeID { get; set; }
        public required string contenido { get; set; }
        public int usuarioEnvioID { get; set; }
        public DateTime fecha { get; set; }
    }
}
