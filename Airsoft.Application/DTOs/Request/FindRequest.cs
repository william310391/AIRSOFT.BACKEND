namespace Airsoft.Application.DTOs.Request
{
    public class FindRequest
    {
        public string? buscar { get; set; }
        public int pagina { get; set; }
        public int tamanoPagina { get; set; }
    }
}
