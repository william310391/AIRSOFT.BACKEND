using Airsoft.Application.DTOs;
using Airsoft.Application.DTOs.Pais;

namespace Airsoft.Application.Interfaces
{
    public interface IPaisService
    {
        Task<ApiResponse<List<PaisResponse>>> GetPaisAll();
    }
}
