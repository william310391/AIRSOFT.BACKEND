namespace Airsoft.Domain.Entities
{
    public class Datos
    {
        public int DatoID { get; set; }
        public required string TipoDato { get; set; }
        public required string DatoNombre { get; set; }
        public required string DatoValor { get; set; }
        public bool Activo { get; set; }
        public int UsuarioRegistroID { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int UsuarioModificacionID { get; set; }
        public DateTime FechaModificacion { get; set; }

    }
}
