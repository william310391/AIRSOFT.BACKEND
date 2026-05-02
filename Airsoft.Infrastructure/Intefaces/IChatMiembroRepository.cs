using Airsoft.Domain.Entities;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IChatMiembroRepository
    {
        Task<bool> Save(ChatMiembro chatMiembro);
    }
}
