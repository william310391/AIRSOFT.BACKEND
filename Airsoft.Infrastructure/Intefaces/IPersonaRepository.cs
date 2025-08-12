using Airsoft.Domain.Entities;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IPersonaRepository
    {
        Task<List<Persona>> GetPersonas();
        Task<Persona> GetPersonaByID(int personaID);
    }
}
