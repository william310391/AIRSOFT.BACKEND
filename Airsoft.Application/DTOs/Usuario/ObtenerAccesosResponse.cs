namespace Airsoft.Application.DTOs.Usuario
{
    public class ObtenerAccesosResponse
    {
        public int usuarioID { get; set; }
        public string? nombreRol { get; set; }
        public string? usuarioCuenta { get; set; }
        public string? usuarioNombre { get; set; }

        public List<MenuPaginaResponse>? listaPagina { get; set; }

    }
}
