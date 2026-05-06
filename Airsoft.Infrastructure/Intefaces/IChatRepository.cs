using Airsoft.Domain.Entities;
using System.Data;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IChatRepository
    {
        Task<bool> Save(Chat chat, IDbTransaction transaction);
    }
}
