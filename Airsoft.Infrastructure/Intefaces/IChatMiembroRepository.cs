using Airsoft.Domain.Entities;
using System.Data;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IChatMiembroRepository
    {
        Task<bool> Save(ChatMiembro chatMiembro, IDbTransaction transaction);
    }
}
