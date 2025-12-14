namespace Airsoft.Application.DTOs.Request
{
    public class DatosRequest
    {
        public int DatoID { get; set; }
        public required string TipoDato { get; set; }
        public required string DatoNombre { get; set; }
        public string? DatoValor { get; set; }
        public bool Activo { get; set; }
    }
}
