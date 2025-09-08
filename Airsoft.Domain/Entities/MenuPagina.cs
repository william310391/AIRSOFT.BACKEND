namespace Airsoft.Domain.Entities
{
    public class MenuPagina
    {
        public int MenuID { get; set; }
        public int PaginaID { get; set; }
        public required string MenuNombre { get; set; }
        public string? MenuIcono { get; set; }
        public string? MenuUrlLink { get; set; }
        public required string PaginaNombre { get; set; }
        public string? PaginaIcono { get; set; }
        public string? PaginaUrlLink { get; set; }
    }
}
