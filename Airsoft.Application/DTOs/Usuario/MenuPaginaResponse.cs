namespace Airsoft.Application.DTOs.Usuario
{
    public class MenuPaginaResponse
    {
        public required string MenuNombre { get; set; }
        public string? MenuIcono { get; set; }
        public string? MenuUrlLink { get; set; }
        public required string PaginaNombre { get; set; }
        public string? PaginaIcono { get; set; }
        public string? PaginaUrlLink { get; set; }
        public required string RutaComponente { get; set; }
    }
}
