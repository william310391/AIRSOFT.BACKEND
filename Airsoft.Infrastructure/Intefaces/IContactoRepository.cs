using Airsoft.Domain.Entities;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IContactoRepository
    {
        Task<List<Contacto>> GetContactosByUsuarioID(int usuarioID);
        Task<List<Contacto>> FindContactoByBuscar(int usuarioID, string buscar);
        Task<bool> Save(int usuarioID, int contactoUsuarioID);
    }
}
