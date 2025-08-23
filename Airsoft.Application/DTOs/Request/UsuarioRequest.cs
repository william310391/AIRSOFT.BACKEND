namespace Airsoft.Application.DTOs.Request
{
    public class UsuarioRequest
    {
        public required string UsuarioNombre { get; set; }
        public required string Contrasena { get; set; }
        public required string RolNombre { get; set; }        
    }
}
