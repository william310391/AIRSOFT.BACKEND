using Airsoft.Domain.Entities;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IPaisRepository
    {
        Task<List<Pais>> GetPaisAll();
    }
}
