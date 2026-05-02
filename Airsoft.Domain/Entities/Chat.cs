namespace Airsoft.Domain.Entities
{
    public class Chat
    {
        public Guid ChatID { get; set; }
        public bool EsPrivado { get; set; }
        public required string  NombreChat { get; set; }
        public bool Activo { get; set; }
        public int UsuarioRegistroID { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int UsuarioModificacionID { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
