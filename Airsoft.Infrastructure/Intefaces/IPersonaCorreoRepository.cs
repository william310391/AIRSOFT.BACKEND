using Airsoft.Domain.Entities;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IPersonaCorreoRepository
    {
        Task<List<PersonaCorreo>> GetByPersonaID(int personaID);
        Task<PersonaCorreo> GetByPersonaCorreoID(int personaCorreoID);
        Task<bool> Save(PersonaCorreo entidad);
        Task<bool> Update(PersonaCorreo entidad);
        Task<bool> ChangeState(int personaCorreoID, bool activo);
    }
}
