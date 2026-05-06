namespace Airsoft.Application.DTOs.Persona
{
    public class PersonaRequest
    {
        public int PersonaID { get; set; }
        public int TipoDocumentoID { get; set; }
        public required string NumeroDocumento { get; set; }
        public required string Nombre { get; set; }
        public required string ApellidoPaterno { get; set; }
        public required string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int SexoID { get; set; }
        public int PaisID { get; set; }
        public int UsuarioID { get; set; }
    }
}
