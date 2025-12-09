namespace Airsoft.Application.DTOs.Response
{
    public class DatosResponse
    {
        public required string TipoDato { get; set; }
        public required string TipoDatoID { get; set; }
        public required string Dato { get; set; }
        public bool Activo { get; set; }
        public string ActivoDescripcion => Activo ? "Activo" : "Inactivo";
        public int UsuarioRegistroID { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int UsuarioModificacionID { get; set; }
        public DateTime FechaModificacion { get; set; }

    }
}
