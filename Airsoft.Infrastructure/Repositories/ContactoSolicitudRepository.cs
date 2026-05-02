using Airsoft.Domain.Entities;
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;
using Airsoft.Infrastructure.Queries;
using Dapper;

namespace Airsoft.Infrastructure.Repositories
{
    public class ContactoSolicitudRepository : IContactoSolicitudRepository
    {
        private readonly DapperContext _context;

        public ContactoSolicitudRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<bool> Save(ContactoSolicitud contactoSolicitud)
        {
            var sql = ContactoSolicitudQueries.Save;
            return await _context.EjecutarQueryAsync(sql, contactoSolicitud);
        }

        public async Task<bool> ChangeStatus(ContactoSolicitud contactoSolicitud)
        {
            var sql = ContactoSolicitudQueries.changeStatus;
            return await _context.EjecutarQueryAsync(sql, contactoSolicitud);
        }

        public async Task<List<ContactoSolicitud>> GetSolicitudPendientesByUsuarioID(int usuarioID)
        {
            var sql = ContactoSolicitudQueries.GetSolicitudPendientesByUsuarioID;
            return await _context.EjecutarAsync(async conn =>
            {
                var result = await conn.QueryAsync<ContactoSolicitud>(sql, new { UsuarioID = usuarioID });
                return result.ToList();
            });
        }
    }
}
