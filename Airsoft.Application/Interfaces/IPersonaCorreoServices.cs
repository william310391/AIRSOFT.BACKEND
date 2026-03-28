using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;

namespace Airsoft.Application.Interfaces
{
    public interface IPersonaCorreoServices
    {
        Task<ApiResponse<List<PersonaCorreoResponse>>> GetByPersonaID(int personaID);
        Task<ApiResponse<PersonaCorreoResponse>> Save(PersonaCorreoRequest request);
        Task<ApiResponse<PersonaCorreoResponse>> Update(PersonaCorreoRequest request);
        Task<ApiResponse<bool>> ChangeState(PersonaCorreoRequest request);
    }
}
