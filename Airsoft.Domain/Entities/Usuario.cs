namespace Airsoft.Domain.Entities
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string? UsuarioNombre { get; set; }
        public string? Contrasena { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int RolID { get; set; }
    }
}
