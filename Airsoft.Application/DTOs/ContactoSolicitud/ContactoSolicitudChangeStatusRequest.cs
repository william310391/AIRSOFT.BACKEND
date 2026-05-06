namespace Airsoft.Application.DTOs.ContactoSolicitud
{
    public class ContactoSolicitudChangeStatusRequest
    {
        public Guid ContactoSolicitudID { get; set; }
        public int usuarioContactoID { get; set; }
        public int estadoID { get; set; }
    }
}
