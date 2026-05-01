namespace Airsoft.Application.DTOs.Response
{
    public class FindContactoByBuscarResponse
    {
        public int usuarioID { get; set; }
        public string? contactoNombre { get; set; }
        public string? contactoCorreo { get; set; }
        public bool noContacto { get; set; }

        private string _imagenPerfilUrl = "https://placehold.co/25x25";
        public string imagenPerfilUrl
        {
            get => _imagenPerfilUrl;
            set => _imagenPerfilUrl = value ?? "https://placehold.co/25x25";
        }
    }
}
