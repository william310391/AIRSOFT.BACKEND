using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;

namespace Airsoft.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<LoginResponse>> Login(LoginRequest request);
        Task<ApiResponse<UsuarioResponse>> Registrar(UsuarioRequest request);
    }
}
