using Airsoft.Domain.Entities;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IPersonaRepository
    {
        Task<List<Persona>> GetPersonas();
        Task<Persona> GetPersonaByID(int personaID);
        Task<bool> Save(Persona entidad);
        Task<bool> Update(Persona entidad);
    }
}
