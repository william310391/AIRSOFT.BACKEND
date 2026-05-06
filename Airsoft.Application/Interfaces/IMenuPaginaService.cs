using Airsoft.Application.DTOs;
using Airsoft.Application.DTOs.Usuario;

namespace Airsoft.Application.Interfaces
{
    public interface IMenuPaginaService
    {
        Task<ApiResponse<List<MenuPaginaResponse>>> GetMenuPaginasByPersonaID(MenuPaginaRequest request);
        Task<ApiResponse<ObtenerAccesosResponse>> ObtenerAccesos(ObtenerAccesosRequest request);
    }
}
