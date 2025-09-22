using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;

namespace Airsoft.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<ApiResponse<List<UsuarioResponse>>> GetUsuarioAll();
        Task<ApiResponse<FindResponse<UsuarioResponse>>> GetUsuarioFind(FindRequest request);
    }
}
