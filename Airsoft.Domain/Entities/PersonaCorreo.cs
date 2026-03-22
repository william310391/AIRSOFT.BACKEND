namespace Airsoft.Domain.Entities
{
    public class PersonaCorreo
    {
        public int PersonaCorreoID { get; set; }
        public required int PersonaID { get; set; }
        public required int TipoCorreoID { get; set; }
        public required string Correo { get; set; }

        public DateTime FechaRegistro { get; set; }
        public int UsuarioRegistroID { get; set; }
        public DateTime? FechaModificion { get; set; }
        public int? UsuarioModeficionID { get; set; }
        public bool Activo { get; set; }

    }
}
