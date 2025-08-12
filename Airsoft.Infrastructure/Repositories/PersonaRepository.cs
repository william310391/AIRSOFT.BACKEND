using Dapper;
using Airsoft.Infrastructure.Persistence;
using Airsoft.Domain.Entities;
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Queries;

namespace Airsoft.Infrastructure.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly DapperContext _context;

        public PersonaRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<Persona>> GetPersonas()
        {
            var sql = SqlQueryMapper.Get(PersonaQueries.GetPersonas);
            var personas = await _context.EjecutarAsync(async conn =>
            {
                return (await conn.QueryAsync<Persona>(sql, null)).ToList();
            });
            return personas;
        }

        public async Task<Persona> GetPersonaByID(int personaID)
        {
            var sql = SqlQueryMapper.Get(PersonaQueries.GetPersonasById);

            var persona = await _context.EjecutarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<Persona>(
                    sql,
                    new { PersonaID = personaID }
                );
            });

            return persona!; // "!" para indicar al compilador que sabes que podría ser null
        }

    }
}
