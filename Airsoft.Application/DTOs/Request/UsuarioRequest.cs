namespace Airsoft.Application.DTOs.Request
{
    public class UsuarioRequest
    {
        public int UsuarioID { get; set; }
        public required string UsuarioCuenta { get; set; }
        public required string UsuarioNombre { get; set; }
        public required string Contrasena { get; set; }
        public required string ContrasenaConfirmar { get; set; }
        public required int RolID { get; set; }
    }
}
