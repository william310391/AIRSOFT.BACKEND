using Airsoft.Domain.Entities;
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;
using Airsoft.Infrastructure.Queries;
using Dapper;

namespace Airsoft.Infrastructure.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly DapperContext _context;

        public RolRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<Rol>> GetAllRol()
        {
            var sql = RolQueries.GetAllRol;
            var lista = await _context.EjecutarAsync(async conn =>
            {
                return (await conn.QueryAsync<Rol>(sql, null)).ToList();
            });
            return lista;
        }


    }
}
