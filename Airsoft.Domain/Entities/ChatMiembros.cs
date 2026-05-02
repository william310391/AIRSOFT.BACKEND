namespace Airsoft.Domain.Entities
{
    public class ChatMiembro
    {
        public Guid ChatID { get; set; }
        public int UsuarioID { get; set; }
        public bool EsAdmin { get; set; }
        public DateTime FechaUltimaConexion { get; set; }
        public DateTime FechaUltimaLectura { get; set; }
        public bool Activo { get; set; }
        public int UsuarioRegistroID { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int UsuarioModificacionID { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
