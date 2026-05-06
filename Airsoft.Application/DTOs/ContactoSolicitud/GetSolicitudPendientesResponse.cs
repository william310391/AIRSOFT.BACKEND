namespace Airsoft.Application.DTOs.ContactoSolicitud
{
    public class GetSolicitudPendientesResponse
    {
        public Guid ContactoSolicitudID { get; set; }
        public int UsuarioID { get; set; }
        public int UsuarioContactoID { get; set; }
        public string? ContactoNombre { get; set; }
        public string? ContactoCorreo { get; set; }
        public bool Activo { get; set; }
        public string? Mensaje { get; set; }
        public bool EsRemitente { get; set; }
        public int UsuarioRegistroID { get; set; }
        public DateTime FechaRegistro { get; set; }

        private string _imagenPerfilUrl = "https://placehold.co/25x25";
        public string imagenPerfilUrl
        {
            get => _imagenPerfilUrl;
            set => _imagenPerfilUrl = value ?? "https://placehold.co/25x25";
        }
    }
}
