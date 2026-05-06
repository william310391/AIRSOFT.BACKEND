using Airsoft.Application.DTOs;
using Airsoft.Application.DTOs.Auth;

namespace Airsoft.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<LoginResponse>> Login(LoginRequest request);
        Task<ApiResponse<ValidarTokenResponse>> ValidarToken();
    }
}
