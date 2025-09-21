using Airsoft.Application.DTOs.Response;

namespace Airsoft.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<ApiResponse<List<UsuarioResponse>>> GetUsuarioAll();
    }
}
