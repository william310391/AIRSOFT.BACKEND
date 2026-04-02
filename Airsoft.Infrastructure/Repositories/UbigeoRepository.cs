using Airsoft.Domain.Entities;
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;
using Airsoft.Infrastructure.Queries;
using Dapper;

namespace Airsoft.Infrastructure.Repositories
{
    public class UbigeoRepository : IUbigeoRepository
    {
        private readonly DapperContext _context;

        public UbigeoRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<Ubigeo>> GetDepartamentos()
        {
            var sql = UbigeoQueries.GetUbigeoDepartamento;
            var lista = await _context.EjecutarAsync(async conn =>
            {
                return (await conn.QueryAsync<Ubigeo>(sql, null)).ToList();
            });
            return lista;
        }

        public async Task<List<Ubigeo>> GetProvincias(int departamentoID)
        {
            var sql = UbigeoQueries.GetUbigeoProvincia;
            var lista = await _context.EjecutarAsync(async conn =>
            {
                return (await conn.QueryAsync<Ubigeo>(sql, new { DepartamentoID = departamentoID })).ToList();
            });
            return lista;
        }

        public async Task<List<Ubigeo>> GetDistritos(int departamentoID, int provinciaID)
        {
            var sql = UbigeoQueries.GetUbigeoDistrito;
            var lista = await _context.EjecutarAsync(async conn =>
            {
                return (await conn.QueryAsync<Ubigeo>(sql, new { DepartamentoID = departamentoID, ProvinciaID = provinciaID })).ToList();
            });
            return lista;
        }
    }
}
