using Airsoft.Application.DTOs.Response;
namespace Airsoft.Application.Interfaces
{
    public interface IPersonaService
    {
        Task<ApiResponse<IEnumerable<PersonaResponse>>> GetPersonas();
        Task<ApiResponse<PersonaResponse>> GetPersonaByID(int personaID);
    }
}
