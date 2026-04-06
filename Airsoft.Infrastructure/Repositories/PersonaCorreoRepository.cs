using Airsoft.Domain.Entities;
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;
using Airsoft.Infrastructure.Queries;
using Dapper;

namespace Airsoft.Infrastructure.Repositories
{
    public class PersonaCorreoRepository: IPersonaCorreoRepository
    {
        private readonly DapperContext _context;

        public PersonaCorreoRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<PersonaCorreo>> GetByPersonaID(int personaID) {
            var sql = PersonaCorreoQueries.GetByPersonaID;
            var result = await _context.EjecutarAsync(async conn =>
            {
                return (await conn.QueryAsync<PersonaCorreo>(sql, new { PersonaID = personaID })).ToList();
            });
            return result;
        }

        public async Task<PersonaCorreo> GetByPersonaCorreoID(int personaCorreoID)
        {
            var sql = PersonaCorreoQueries.GetByPersonaCorreoID;
            var entidad = await _context.EjecutarAsync(async conn => {
                return (await conn.QueryAsync<PersonaCorreo>(sql, new { PersonaCorreoID = personaCorreoID })).FirstOrDefault();
            });
            return entidad!;
        }

        public async Task<bool> Save(PersonaCorreo entidad) 
        {
            var sql = PersonaCorreoQueries.Save;
            return await _context.EjecutarQueryAsync(sql, entidad);
        }

        public async Task<bool> Update(PersonaCorreo entidad)
        {
            var sql = PersonaCorreoQueries.Update;
            return await _context.EjecutarQueryAsync(sql, entidad);
        }

        public async Task<bool> ChangeState(int personaCorreoID,int usuarioID, bool activo)
        {
            var sql = PersonaCorreoQueries.ChangeState;
            return await _context.EjecutarQueryAsync(sql, new { PersonaCorreoID = personaCorreoID, UsuarioModeficionID= usuarioID, Activo = activo });
        }
    }
}
