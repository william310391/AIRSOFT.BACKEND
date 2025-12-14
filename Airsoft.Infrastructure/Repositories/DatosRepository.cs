
using Airsoft.Domain.Entities;
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;
using Airsoft.Infrastructure.Queries;
using Dapper;

namespace Airsoft.Infrastructure.Repositories
{
    public class DatosRepository : IDatosRepository
    {
        private readonly DapperContext _context;

        public DatosRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<(List<Datos> Datos, int TotalRegistros)> FindBuscarDato(string? buscar, int pagina, int tamañoPagina)
        {
            var sql = SqlQueryMapper.Get(DatosQueries.FindBuscarDato);

            return await _context.EjecutarAsync(async conn =>
            {
                using var multi = await conn.QueryMultipleAsync(sql, new
                {
                    Skip = (pagina - 1) * tamañoPagina,
                    Take = tamañoPagina,
                    Buscar = buscar
                });

                var datos = (await multi.ReadAsync<Datos>()).ToList();
                var total = await multi.ReadFirstAsync<int>();

                return (datos, total);
            });
        }

        public async Task<List<Datos>> FindByTipoDato(string tipoDato)
        {
            var sql = SqlQueryMapper.Get(DatosQueries.FindByTipoDato);
            return await _context.EjecutarAsync(async conn =>
            {
                var result = await conn.QueryAsync<Datos>(sql, new { TipoDato = tipoDato });
                return result.ToList();
            });
        }

        public async Task<bool> Save(Datos datos)
        {
            var sql = SqlQueryMapper.Get(DatosQueries.Save);
            return await _context.EjecutarQueryAsync(sql, datos);
        }

        public async Task<bool> Update(Datos datos)
        {
            var sql = SqlQueryMapper.Get(DatosQueries.Update);
            return await _context.EjecutarQueryAsync(sql, datos);
        }
        public async Task<bool> ExistsDato(string tipoDato, string datoID)
        {
            var sql = SqlQueryMapper.Get(DatosQueries.ExistsDato);     
            return await _context.EjecutarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<bool>(
                    sql,
                    new { TipoDato = tipoDato, DatoID = datoID }
                );
            });
        }


    }
}
