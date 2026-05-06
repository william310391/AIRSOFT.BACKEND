namespace Airsoft.Application.DTOs.Pais
{
    public class PaisResponse
    {
        public int PaisID { get; set; }
        public required string Nombre { get; set; }
        public required string NombreOficial { get; set; }
    }
}
