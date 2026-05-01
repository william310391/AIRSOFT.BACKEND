namespace Airsoft.Application.DTOs.Response
{
    public class GetContactosByUsuarioIDResponse
    {
        public int usuarioContactoID { get; set; }
        public string? contactoNombre { get; set; }

        public string? contactoCorreo { get; set; }

        private string _imagenPerfilUrl = "https://placehold.co/25x25";
        public string imagenPerfilUrl
        {
            get => _imagenPerfilUrl;
            set => _imagenPerfilUrl = value ?? "https://placehold.co/25x25";
        }
    }
}
