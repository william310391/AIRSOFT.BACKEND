using Airsoft.Application.DTOs.Response;

namespace Airsoft.Application.Interfaces
{
    public interface IUbigeoService
    {
        Task<ApiResponse<List<UbigeoResponse>>> GetDepartamentos();
        Task<ApiResponse<List<UbigeoResponse>>> GetProvincias(int departamentoId);
        Task<ApiResponse<List<UbigeoResponse>>> GetDistritos(int departamentoId, int provinciaId);
    }
}
