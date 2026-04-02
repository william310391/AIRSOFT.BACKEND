using Airsoft.Application.DTOs.Response;

namespace Airsoft.Application.Interfaces
{
    public interface IPaisService
    {
        Task<ApiResponse<List<PaisResponse>>> GetPaisAll();
    }
}
