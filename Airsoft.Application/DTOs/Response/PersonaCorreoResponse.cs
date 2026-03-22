namespace Airsoft.Application.DTOs.Response
{
    public class PersonaCorreoResponse
    {
        public int PersonaCorreoID { get; set; }
        public required int PersonaID { get; set; }
        public required int TipoCorreoID { get; set; }
        public required string TipoCorreo { get; set; }
        public required string Correo { get; set; }

        public DateTime FechaRegistro { get; set; }
        public int UsuarioRegistroID { get; set; }
        public DateTime? FechaModificion { get; set; }
        public int? UsuarioModeficionID { get; set; }
        public bool Activo { get; set; }
        public string ActivoDescripcion => Activo ? "Activo" : "Inactivo";
    }
}
