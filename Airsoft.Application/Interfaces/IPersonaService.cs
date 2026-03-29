using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;
namespace Airsoft.Application.Interfaces
{
    public interface IPersonaService
    {
        Task<ApiResponse<IEnumerable<PersonaResponse>>> GetPersonas();
        Task<ApiResponse<PersonaResponse>> GetPersonaByID(int personaID);
        Task<ApiResponse<bool>> Save(PersonaRequest request);
        Task<ApiResponse<bool>> Update(PersonaRequest request);
    }
}
