namespace Airsoft.Application.DTOs.Request
{
    public class DatosRequest
    {
        public required string TipoDato { get; set; }
        public required string DatoID { get; set; }
        public required string DatoNombre { get; set; }
        public required string DatoValor { get; set; }
        public bool Activo { get; set; }
    }
}
