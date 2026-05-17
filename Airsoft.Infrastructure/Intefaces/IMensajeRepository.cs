using Airsoft.Domain.Entities;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IMensajeRepository
    {
        Task<List<Mensaje>> GetMensaje(int pageSize, Guid chatID);
        Task<bool> Save(Mensaje mensaje);
        Task<bool> Update(Mensaje mensaje);
        Task<bool> Delete(Mensaje mensaje);
    }
}
