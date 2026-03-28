using Airsoft.Domain.Entities;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IPersonaTelefonoRepository
    {
        Task<List<PersonaTelefono>> GetByPersonaID(int personaID);
        Task<PersonaTelefono> GetByPersonaTelefonoID(int personaTelefonoID);
        Task<bool> Save(PersonaTelefono entidad);
        Task<bool> Update(PersonaTelefono entidad);
        Task<bool> ChangeState(int personaTelefonoID, bool activo);
    }
}
