namespace Airsoft.Application.DTOs.Request
{
    public class PersonaCorreoRequest
    {
        public int PersonaCorreoID { get; set; }
        public required int PersonaID { get; set; }
        public required int TipoCorreoID { get; set; }
        public required string Correo { get; set; }
        public int UsuarioID { get; set; }
        public bool Activo { get; set; }
    }
}
