namespace Airsoft.Application.DTOs.Response
{
    public class UsuarioResponse
    {
        public int UsuarioID { get; set; }
        public required string UsuarioCuenta { get; set; }
        public required string UsuarioNombre { get; set; }
        public string? RolNombre { get; set; }
        public int RolID { get; set; }
        public bool Estado { get; set; }
        public string EstadoDescripcion => Estado ? "Activo" : "Inactivo";
    }
}
