namespace Airsoft.Application.DTOs.Response
{
    public class LoginResponse
    {
        public string? Token { get; set; }
        public string? NombreUsuario { get; set; }
        public int UsuarioId { get; set; }
    }
}
