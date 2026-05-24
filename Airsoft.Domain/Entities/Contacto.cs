namespace Airsoft.Domain.Entities
{
    public class Contacto
    {
        public int ContactoID { get; set; }
        public int UsuarioID { get; set; }
        public int UsuarioContactoID { get; set; }
        public string? ContactoNombre { get; set; }
        public string? ContactoCorreo { get; set; }       
        public bool Activo { get; set; }
        public int UsuarioRegistroID { get; set; }
        public bool NoContacto { get; set; }
        public bool SolicitudPendiente { get; set; }
        public bool EsRemitente { get; set; }
        public Guid? ContactoSolicitudID { get; set; }
        public int SolicitudUsuarioID { get; set; }
        public int SolicitudUsuarioContactoID { get; set; }
        public string? SolicitudMensaje { get; set; }
        public string? TipoRegistro { get; set; }

        public Guid? ChatID { get; set; }
        public string? NombreChat { get; set; }
        public bool EsPrivado { get; set; }
        public int MensajeNoleidos { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int UsuarioModificacionID { get; set; }
        public DateTime FechaModificacion { get; set; }

    }
}
