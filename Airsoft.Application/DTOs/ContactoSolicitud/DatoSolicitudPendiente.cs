namespace Airsoft.Application.DTOs.ContactoSolicitud
{
    public class DatoSolicitudPendiente
    {
        public bool SolicitudPendiente { get; set; }
        public bool EsRemitente { get; set; }
        public Guid? contactoSolicitudID { get; set; }
        public int solicitudUsuarioID { get; set; }
        public int solicitudUsuarioContactoID { get; set; }
        public string? solicitudMensaje { get; set; }

    }
}
