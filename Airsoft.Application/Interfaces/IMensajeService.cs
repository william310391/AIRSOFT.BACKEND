using Airsoft.Application.DTOs;
using Airsoft.Application.DTOs.Mensaje;

namespace Airsoft.Application.Interfaces
{
    public interface IMensajeService
    {
        Task<ApiResponse<List<MensajeResponse>>> GetMensajesByChatID(GetMensajeRequest request);
        Task<ApiResponse<MensajeResponse>> Save(MensajeSaveRequest request);
        Task<ApiResponse<bool>> Update(MensajeUpdateRequest request);
        Task<ApiResponse<bool>> Delete(MensajeDeleteRequest request);
    }
}
