namespace Airsoft.Application.DTOs.Response
{
    public class FindResponse<T>
    {
        public FindResponse()
        {
            datos = new List<T>();
        }

        public List<T> datos { get; set; }
        public int pagina { get; set; }
        public int tamanoPagina { get; set; }
        public int totalRegistros { get; set; }

        public int totalPaginas =>
            tamanoPagina == 0 ? 0 : (int)Math.Ceiling((double)totalRegistros / tamanoPagina);

        public bool tienePaginaAnterior => pagina > 1;
        public bool tienePaginaSiguiente => pagina < totalPaginas;
    }
}
