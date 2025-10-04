namespace Airsoft.Application.DTOs.Response
{
    public class LoginResponse
    {
        public string? Token { get; set; }
        public string? UsuarioCuenta { get; set; }
        public string? UsuarioNombre { get; set; }
        public int UsuarioId { get; set; }
    }
}
