namespace Airsoft.Application.DTOs.Request
{
    public class ContactoSolicitudChangeStatusRequest
    {
        public Guid ContactoSolicitudID { get; set; }
        public int usuarioContactoID { get; set; }
        public int estadoID { get; set; }
    }
}
