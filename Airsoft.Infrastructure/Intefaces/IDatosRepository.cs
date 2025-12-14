using Airsoft.Domain.Entities;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IDatosRepository
    {
        Task<(List<Datos> Datos, int TotalRegistros)> FindBuscarDato(string? buscar, int pagina, int tamañoPagina);
        Task<List<Datos>> FindByTipoDato(string tipoDato);
        Task<bool> Save(Datos datos);
        Task<bool> Update(Datos datos);
        Task<bool> ExistsDato(string tipoDato, string datoID);
    }
}
