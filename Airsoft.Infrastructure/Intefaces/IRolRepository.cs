using Airsoft.Domain.Entities;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IRolRepository
    {
        Task<List<Rol>> GetAllRol();
    }
}
