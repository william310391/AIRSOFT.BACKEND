namespace Airsoft.Application.DTOs.Response
{
    public class UbigeoResponse
    {
        public int UbigeoID { get; set; }
        public string? UbigeoCodigo { get; set; }
        public int DepartamentoID { get; set; }
        public int ProvinciaID { get; set; }
        public int DistritoID { get; set; }
        public string? DepartamentoNombre { get; set; }
        public string? ProvinciaNombre { get; set; }
        public string? DistritoNombre { get; set; }
        public string? NombreCapital { get; set; }
        public int RegionNaturalID { get; set; }
        public string? RegionNaturalNombre { get; set; }
    }
}
