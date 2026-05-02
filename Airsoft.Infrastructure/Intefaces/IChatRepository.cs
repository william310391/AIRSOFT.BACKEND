using Airsoft.Domain.Entities;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IChatRepository
    {
        Task<bool> Save(Chat chat);
    }
}
