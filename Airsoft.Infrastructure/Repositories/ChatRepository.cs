using Airsoft.Domain.Entities;
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;
using Airsoft.Infrastructure.Queries;
using System.Data;

namespace Airsoft.Infrastructure.Repositories
{
    public class ChatRepository: IChatRepository
    {
        private readonly DapperContext _context;

        public ChatRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<bool> Save(Chat chat, IDbTransaction transaction)
        {
            var sql = ChatQueries.Save;
            return await _context.EjecutarQueryAsync(sql, chat, transaction);
        }
    }
}
