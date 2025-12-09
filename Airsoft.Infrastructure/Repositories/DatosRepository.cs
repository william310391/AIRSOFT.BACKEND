
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

        public async Task<Datos> FindByDatoID(int datoID)
        {
            var sql = SqlQueryMapper.Get(DatosQueries.FindAll);
            var entidad = await _context.EjecutarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<Datos>(
                    sql,
                    new { DatoID = datoID }
                );
            });
            return entidad!; // "!" para indicar al compilador que sabes que podría ser null
        }


    }
}
