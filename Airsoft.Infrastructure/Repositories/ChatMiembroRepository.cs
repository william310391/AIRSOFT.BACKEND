using Airsoft.Domain.Entities;
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;
using Airsoft.Infrastructure.Queries;
using System.Data;

namespace Airsoft.Infrastructure.Repositories
{
    public class ChatMiembroRepository: IChatMiembroRepository
    {
        private readonly DapperContext _context;

        public ChatMiembroRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<bool> Save(ChatMiembro chatMiembro, IDbTransaction transaction)
        {
            var sql = ChatMiembroQueries.Save;
            return await _context.EjecutarQueryAsync(sql, chatMiembro, transaction);
        }

    }
}
