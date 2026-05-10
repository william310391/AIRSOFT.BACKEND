namespace Airsoft.Application.DTOs.Contacto
{
    public class FindContactoByBuscarResponse
    {
        public int usuarioID { get; set; }
        public string? contactoNombre { get; set; }
        public string? contactoCorreo { get; set; }
        public bool noContacto { get; set; }
        public bool solicitudPendiente { get; set; }
        public bool esRemitente { get; set; }
        public Guid? contactoSolicitudID { get; set; }
        public int solicitudUsuarioID { get; set; }
        public int solicitudUsuarioContactoID { get; set; }
        public Guid? chatID { get; set; }

        private string _imagenPerfilUrl = "https://placehold.co/25x25";
        public string imagenPerfilUrl
        {
            get => _imagenPerfilUrl;
            set => _imagenPerfilUrl = value ?? "https://placehold.co/25x25";
        }
    }
}
