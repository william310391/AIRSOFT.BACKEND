using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;

namespace Airsoft.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<ApiResponse<List<UsuarioResponse>>> GetUsuarioAll();
        Task<ApiResponse<FindResponse<UsuarioResponse>>> GetUsuarioFind(FindRequest request);
        Task<ApiResponse<UsuarioResponse>> Create(UsuarioRequest request);
        Task<ApiResponse<List<RolResponse>>> GetRol();
    }
}
