using Airsoft.Domain.Entities;
using System.Data;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IContactoSolicitudRepository
    {
        Task<bool> Save(ContactoSolicitud contactoSolicitud);
        Task<bool> ChangeStatus(ContactoSolicitud contactoSolicitud);
        Task<List<ContactoSolicitud>> GetSolicitudPendientesByUsuarioID(int usuarioID);
    }
}
