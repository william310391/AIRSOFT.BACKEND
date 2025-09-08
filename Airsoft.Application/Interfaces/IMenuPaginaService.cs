using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;

namespace Airsoft.Application.Interfaces
{
    public interface IMenuPaginaService
    {
        Task<ApiResponse<List<MenuPaginaResponse>>> GetMenuPaginasByPersonaID(MenuPaginaRequest request);
        Task<ApiResponse<ObtenerAccesosResponse>> ObtenerAccesos(ObtenerAccesosRequest request);
    }
}
