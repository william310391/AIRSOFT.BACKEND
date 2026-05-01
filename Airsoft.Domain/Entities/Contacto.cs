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
        public DateTime FechaRegistro { get; set; }
        public int UsuarioModificacionID { get; set; }
        public DateTime FechaModificacion { get; set; }

    }
}
