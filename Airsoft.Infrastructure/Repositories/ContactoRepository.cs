using Airsoft.Domain.Entities;
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;
using Airsoft.Infrastructure.Queries;
using Dapper;

namespace Airsoft.Infrastructure.Repositories
{
    public class ContactoRepository : IContactoRepository
    {
        private readonly DapperContext _context;

        public ContactoRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<Contacto>> GetContactosByUsuarioID(int usuarioID)
        {
            var sql = ContactoQueres.GetContactosByUsuarioID;
            return await _context.EjecutarAsync(async conn =>
            {
                var result = await conn.QueryAsync<Contacto>(sql, new { UsuarioID = usuarioID });
                return result.ToList();
            });
        }

        public async Task<List<Contacto>> FindContactoByBuscar(int usuarioID, string buscar)
        {
            var sql = ContactoQueres.FindContacto;
            return await _context.EjecutarAsync(async conn =>
            {
                var result = await conn.QueryAsync<Contacto>(sql, new { UsuarioID = usuarioID, Buscar = buscar });
                return result.ToList();
            });
        }

        public async Task<bool> Save(int usuarioID, int contactoUsuarioID)
        {
            var sql = ContactoQueres.Save;
            return await _context.EjecutarQueryAsync(sql, new { UsuarioID = usuarioID, ContactoUsuarioID = contactoUsuarioID });

        }
    }
}
