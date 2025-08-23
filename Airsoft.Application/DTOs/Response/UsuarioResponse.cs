namespace Airsoft.Application.DTOs.Response
{
    public class UsuarioResponse
    {
        public int UsuarioID { get; set; }
        public required string UsuarioNombre { get; set; }
        public string? RolNombre { get; set; }
    }
}
