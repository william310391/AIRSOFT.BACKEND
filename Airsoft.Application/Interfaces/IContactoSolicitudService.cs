using Airsoft.Application.DTOs.ContactoSolicitud;
using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;

namespace Airsoft.Application.Interfaces
{
    public interface IContactoSolicitudService
    {
        Task<ApiResponse<bool>> Create(ContactoSolicitudSaveRequest request);
        Task<ApiResponse<bool>> ChangeStatus(ContactoSolicitudChangeStatusRequest request);
        Task<ApiResponse<List<GetSolicitudPendientesResponse>>> GetSolicitudPendientes();

    }
}
