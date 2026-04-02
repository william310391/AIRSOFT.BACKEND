namespace Airsoft.Domain.Entities
{
    public class Pais
    {
        public int PaisID { get; set; }
        public required string CodigoIso2 { get; set; }
        public required string CodigoIso3 { get; set; }
        public required string Nombre { get; set; }
        public required string NombreOficial { get; set; }
        public required string Continente { get; set; }
        public required bool Activo { get; set; }
    }
}
