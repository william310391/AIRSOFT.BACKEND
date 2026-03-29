 

namespace Airsoft.Domain.Entities
{
    public class Persona
    {
        public int PersonaID { get; set; }
        public int TipoDocumentoID { get; set; }
        public string? TipoDocumento { get; set; }
        public required string NumeroDocumento { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int SexoID { get; set; }
        public string? Sexo { get; set; }
        public int PaisID { get; set; }
        public string? Pais {  get; set; }
        public int UsuarioRegistroID { get; set; }
        public DateTime? UsuarioRegistro { get; set; }
        public int UsuarioModeficionID { get; set; }
        public DateTime? FechaModificion {  get; set; }


    }
}
