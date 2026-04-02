using Airsoft.Domain.Entities;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IUbigeoRepository
    {
        Task<List<Ubigeo>> GetDepartamentos();
        Task<List<Ubigeo>> GetProvincias(int departamentoId);
        Task<List<Ubigeo>> GetDistritos(int departamentoId, int provinciaId);
    }
}
