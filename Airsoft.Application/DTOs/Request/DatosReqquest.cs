namespace Airsoft.Application.DTOs.Request
{
    public class DatosReqquest
    {
        public required string TipoDato { get; set; }
        public required string TipoDatoID { get; set; }
        public required string Dato { get; set; }
        public bool Activo { get; set; }
    }
}
