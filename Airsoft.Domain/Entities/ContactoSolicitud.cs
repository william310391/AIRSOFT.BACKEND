namespace Airsoft.Domain.Entities
{
    public class ContactoSolicitud
    {
        public int ContactoSolicitudID { get; set; }
        public int UsuarioID { get; set; }
        public int UsuarioContactoID { get; set; }
        public int EstadoID { get; set; }
        public bool Activo { get; set; }
        public int UsuarioRegistroID { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int UsuarioModificacionID { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
