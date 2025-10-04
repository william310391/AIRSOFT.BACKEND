namespace Airsoft.Application.DTOs.Response
{
    public class UsuarioResponse
    {
        public int UsuarioID { get; set; }
        public required string UsuarioCuenta { get; set; }
        public required string UsuarioNombre { get; set; }
        public string? RolNombre { get; set; }
        public int RolID { get; set; }
        public bool Activo { get; set; }
        public string ActivoDescripcion => Activo ? "Activo" : "Inactivo";
    }
}
