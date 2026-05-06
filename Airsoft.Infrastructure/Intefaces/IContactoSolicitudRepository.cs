using Airsoft.Domain.Entities;
using System.Data;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IContactoSolicitudRepository
    {
        Task<bool> Save(ContactoSolicitud contactoSolicitud);
        Task<bool> ChangeStatusByUsuarioID(ContactoSolicitud contactoSolicitud);
        Task<bool> ChangeStatusByUsuarioID(ContactoSolicitud contactoSolicitud, IDbTransaction transaction);
        Task<bool> ChangeStatusByContactoSolicitudID(ContactoSolicitud contactoSolicitud, IDbTransaction transaction);
        Task<List<ContactoSolicitud>> GetSolicitudPendientesByUsuarioID(int usuarioID);

    }
}
