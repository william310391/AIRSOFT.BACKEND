using Airsoft.Application.DTOs.ContactoSolicitud;
namespace Airsoft.Application.DTOs.Contacto
{
    public class ContatoDetalleResponse
    {
        public Guid? contactoSolicitudID { get; set; }
        public int usuarioID { get; set; }
        public int usuarioContactoID { get; set; }
        public string? contactoNombre { get; set; }
        public string? contactoCorreo { get; set; }
        public bool noContacto { get; set; }
        public Guid? ChatID { get; set; }
        public string? tipoRegistro { get; set; }
        public DatoSolicitudPendiente? datosSolicitudPendiente { get; set; }

        private string _imagenPerfilUrl = "https://placehold.co/25x25";
        public string imagenPerfilUrl
        {
            get => _imagenPerfilUrl;
            set => _imagenPerfilUrl = value ?? "https://placehold.co/25x25";
        }
    }
}
