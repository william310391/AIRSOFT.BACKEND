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

        public async Task<Usuario> GetUsuarioByUsuarioCuenta(string usuarioCuenta)
        {
            var sql = UsuarioQueries.GetUsuariosByUsuarioNombre;

            var entidad = await _context.EjecutarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<Usuario>(
                    sql,
                    new { UsuarioCuenta = usuarioCuenta }
                );
            });

            return entidad!; // "!" para indicar al compilador que sabes que podría ser null
        }

        public async Task<List<Usuario>> GetUsuariosAll()
        {
            var sql = UsuarioQueries.GetUsuariosAll;

            return await _context.EjecutarAsync(async conn =>
            {
                var result = await conn.QueryAsync<Usuario>(sql);
                return result.ToList(); 
            });
        }

        public async Task<(List<Usuario> Usuarios, int TotalRegistros)> GetUsuarioFind(string? buscar, int pagina, int tamañoPagina)
        {
            var sql = UsuarioQueries.GetUsuariosFind;

            return await _context.EjecutarAsync(async conn =>
            {
                using var multi = await conn.QueryMultipleAsync(sql, new
                {
                    Skip = (pagina - 1) * tamañoPagina,
                    Take = tamañoPagina,
                    Buscar = buscar
                });

                var usuarios = (await multi.ReadAsync<Usuario>()).ToList();
                var total = await multi.ReadFirstAsync<int>();

                return (usuarios, total);
            });
        }


        public async Task<Usuario> GetUsuariosByUsuarioID(int usuarioID)
        {
            var sql = UsuarioQueries.GetUsuariosByUsuarioID;

            var entidad = await _context.EjecutarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<Usuario>(
                    sql,
                    new { UsuarioID = usuarioID }
                );
            });

            return entidad!; // "!" para indicar al compilador que sabes que podría ser null
        }

        public async Task<bool> ExistsUsuario(string usuarioCuenta)
        {
            var sql = UsuarioQueries.ExistsUasuario;
            return await _context.EjecutarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<bool>(
                    sql,
                    new { UsuarioCuenta = usuarioCuenta }
                );
            });
        }
        public async Task<bool> SaveUsuario(Usuario usuario)
        {
            var sql = UsuarioQueries.SaveUsuario;
            return await _context.EjecutarQueryAsync(sql, usuario);
        }

        public async Task<bool> UpdateUsuario(Usuario usuario)
        {
            var sql = UsuarioQueries.UpdateUsuario;
            return await _context.EjecutarQueryAsync(sql, usuario);
        }

        public async Task<bool> DeleteUsuario(int usuarioID)
        {
            var sql = UsuarioQueries.DeleteUsuario;
            return await _context.EjecutarQueryAsync(sql,new { UsuarioID= usuarioID});
        }

        public async Task<bool> ChangeState(int usuarioID, bool estado)
        {
            var sql = UsuarioQueries.ChangeState;
            return await _context.EjecutarQueryAsync(sql, new { Estado = estado, UsuarioID = usuarioID });
        }

    }
}
