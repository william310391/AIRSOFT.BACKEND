namespace Airsoft.Domain.Entities
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public required string UsuarioCuenta { get; set; }
        public required string UsuarioNombre { get; set; }
        public required string Contrasena { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int RolID { get; set; }
        public string? RolNombre { get; set; }
        public bool Activo { get; set; }
    }
}
