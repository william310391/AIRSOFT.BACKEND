using Airsoft.Domain.Entities;
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;
using Airsoft.Infrastructure.Queries;
using Dapper;

namespace Airsoft.Infrastructure.Repositories
{
    public class MensajeRepository : IMensajeRepository
    {
        private readonly DapperContext _context;

        public MensajeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<Mensaje>> GetMensaje(int pageSize, Guid chatID)
        {
            var sql = MensajeQueries.GetMensaje;
            return await _context.EjecutarAsync(async conn =>
            {
                var result = await conn.QueryAsync<Mensaje>(sql, new { PageNumber = 1, PageSize = pageSize, ChatID = chatID });
                return result.ToList();
            });
        }

        public async Task<bool> Save(Mensaje mensaje)
        {
            var sql = MensajeQueries.Save;
            return await _context.EjecutarQueryAsync(sql, mensaje);
        }

        public async Task<bool> Update(Mensaje mensaje)
        {
            var sql = MensajeQueries.Update;
            return await _context.EjecutarQueryAsync(sql, mensaje);
        }

        public async Task<bool> Delete(Mensaje mensaje)
        {
            var sql = MensajeQueries.Delete;
            return await _context.EjecutarQueryAsync(sql, mensaje);
        }


    }
}
