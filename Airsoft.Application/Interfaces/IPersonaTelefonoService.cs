
using Airsoft.Application.DTOs;
using Airsoft.Application.DTOs.PersonaTelefono;

namespace Airsoft.Application.Interfaces
{
    public interface IPersonaTelefonoService
    {
        Task<ApiResponse<List<PersonaTelefonoResponse>>> GetByPersonaID(int personaID);
        Task<ApiResponse<PersonaTelefonoResponse>> Save(PersonaTelefonoRequest request);
        Task<ApiResponse<PersonaTelefonoResponse>> Update(PersonaTelefonoRequest request);
        Task<ApiResponse<bool>> ChangeState(PersonaTelefonoRequest request);

    }
}
