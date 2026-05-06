using Airsoft.Domain.Entities;
using System.Data;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IContactoRepository
    {
        Task<List<Contacto>> GetContactosByUsuarioID(int usuarioID);
        Task<List<Contacto>> FindContactoByBuscar(int usuarioID, string buscar);
        Task<bool> Save(int usuarioID, int contactoUsuarioID, IDbTransaction transaction);
    }
}
