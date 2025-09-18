namespace Airsoft.Application.DTOs.Response
{
    public class ObtenerAccesosResponse
    {
        public int usuarioID { get; set; }
        public string? nombreRol { get; set; }
        public string? nombreUsuario { get; set; }
        public List<MenuPaginaResponse>? listaPagina { get; set; }

    }
}
