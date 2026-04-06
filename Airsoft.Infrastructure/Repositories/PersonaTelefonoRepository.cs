using Airsoft.Domain.Entities;
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;
using Airsoft.Infrastructure.Queries;
using Dapper;

namespace Airsoft.Infrastructure.Repositories
{
    public class PersonaTelefonoRepository: IPersonaTelefonoRepository
    {
        private readonly DapperContext _context;

        public PersonaTelefonoRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<PersonaTelefono>> GetByPersonaID(int personaID)
        {
            var sql = PersonaTelefonoQueries.GetByPersonaID;
            var result = await _context.EjecutarAsync(async conn =>
            {
                return (await conn.QueryAsync<PersonaTelefono>(sql, new { PersonaID = personaID })).ToList();
            });
            return result;
        }
        public async Task<PersonaTelefono> GetByPersonaTelefonoID(int personaTelefonoID)
        {
            var sql = PersonaTelefonoQueries.GetByPersonaTelefonoID;
            var entidad = await _context.EjecutarAsync(async conn =>
            {
                return (await conn.QueryAsync<PersonaTelefono>(sql, new { PersonaTelefonoID = personaTelefonoID })).FirstOrDefault();
            });
            return entidad!;
        }

        public async Task<bool> Save(PersonaTelefono entidad)
        {
            var sql = PersonaTelefonoQueries.Save;
            return await _context.EjecutarQueryAsync(sql, entidad);
        }

        public async Task<bool> Update(PersonaTelefono entidad)
        {
            var sql = PersonaTelefonoQueries.Update;
            return await _context.EjecutarQueryAsync(sql, entidad);
        }

        public async Task<bool> ChangeState(int personaTelefonoID,int personaID, bool activo)
        {
            var sql = PersonaTelefonoQueries.ChangeState;
            return await _context.EjecutarQueryAsync(sql, new { PersonaTelefonoID = personaTelefonoID,PersonaID = personaID, Activo = activo });
        }
    }
}
