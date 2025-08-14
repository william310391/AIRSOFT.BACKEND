using Airsoft.Domain.Entities;
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;
using Airsoft.Infrastructure.Queries;
using Dapper;
namespace Airsoft.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DapperContext _context;
        public UsuarioRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetUsuariosByUsuarioNombre(string usuarioNombre)
        {
            var sql = SqlQueryMapper.Get(UsuarioQueries.GetUsuariosByUsuarioNombre);

            var entidad = await _context.EjecutarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<Usuario>(
                    sql,
                    new { UsuarioNombre = usuarioNombre }
                );
            });

            return entidad!; // "!" para indicar al compilador que sabes que podría ser null
        }

    }
}
