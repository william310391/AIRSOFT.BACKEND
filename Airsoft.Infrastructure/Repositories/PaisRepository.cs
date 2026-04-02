using Airsoft.Domain.Entities;
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;
using Airsoft.Infrastructure.Queries;
using Dapper;

namespace Airsoft.Infrastructure.Repositories
{
    public class PaisRepository: IPaisRepository
    {
        private readonly DapperContext _context;
        public PaisRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<Pais>> GetPaisAll()
        {
            var sql = PaisQueries.GetPaisAll;

            return await _context.EjecutarAsync(async conn =>
            {
                var result = await conn.QueryAsync<Pais>(sql);
                return result.ToList();
            });
        }
    }
}
