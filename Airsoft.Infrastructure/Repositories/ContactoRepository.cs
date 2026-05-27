using Airsoft.Domain.Entities;
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;
using Airsoft.Infrastructure.Queries;
using Dapper;
using System.Data;

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
            var sql = ContactoQueres.GetContacto;
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

        public async Task<bool> Save(int usuarioID, int usuarioContactoID, Guid chatID, IDbTransaction transaction)
        {
            var sql = ContactoQueres.Save;
            return await _context.EjecutarQueryAsync(sql, new { UsuarioID = usuarioID, UsuarioContactoID = usuarioContactoID, ChatID= chatID }, transaction);
        }
    }
}
