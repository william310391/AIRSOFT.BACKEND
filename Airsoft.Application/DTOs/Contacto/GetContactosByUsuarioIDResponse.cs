using Airsoft.Application.DTOs.ContactoSolicitud;

namespace Airsoft.Application.DTOs.Contacto
{
    public class GetContactosByUsuarioIDResponse
    {
        public int usuarioContactoID { get; set; }
        public int usuarioID { get; set; }
        public string? contactoNombre { get; set; }
        public string? contactoCorreo { get; set; }
        public bool noContacto { get; set; }

        public Guid? ChatID { get; set; }
        public int UsuarioRegistroID { get; set; }
        public DateTime FechaRegistro { get; set; }

        public DatoSolicitudPendiente? datosSolicitudPendiente { get; set; }

        private string _imagenPerfilUrl = "https://placehold.co/25x25";
        public string imagenPerfilUrl
        {
            get => _imagenPerfilUrl;
            set => _imagenPerfilUrl = value ?? "https://placehold.co/25x25";
        }
    }
}
