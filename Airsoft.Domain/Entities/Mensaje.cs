namespace Airsoft.Domain.Entities
{
    public class Mensaje
    {
        public Guid MensajeID { get; set; }       
        public required string Contenido { get; set; }
        public int UsuarioEnvioID { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEdicion { get; set; }
        public DateTime FechaEliminacion { get; set; }

    }
}
