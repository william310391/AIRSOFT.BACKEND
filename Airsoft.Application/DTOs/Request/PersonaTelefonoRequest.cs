namespace Airsoft.Application.DTOs.Request
{
    public class PersonaTelefonoRequest
    {
        public int PersonaTelefonoID { get; set; }
        public int TipoTelefonoID { get; set; }
        public int PersonaID { get; set; }
        public int PaisID { get; set; }
        public required string Telefono { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int UsuarioRegistroID { get; set; }
        public DateTime? FechaModificion { get; set; }
        public int? UsuarioModeficionID { get; set; }
        public bool Activo { get; set; }
    }
}
