using Airsoft.Domain.Entities;
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;
using Airsoft.Infrastructure.Queries;
using Dapper;

namespace Airsoft.Infrastructure.Repositories
{
    public class MenuPaginaRepository : IMenuPaginaRepository
    {
        private readonly DapperContext _context;

        public MenuPaginaRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<MenuPagina>> GetMenuPaginasByPersonaID(int usuarioID, int rolID)
        {
            var sql = MenuPaginaQueries.GetMenuPaginasByPersonaID;
            var lista = await _context.EjecutarAsync(async conn =>
            {
                return (await conn.QueryAsync<MenuPagina>(sql, new { UsuarioID = usuarioID, RolID =rolID })).ToList();
            });
            return lista;
        }

    }
}
