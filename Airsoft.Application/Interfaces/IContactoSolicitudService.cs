using Airsoft.Application.DTOs;
using Airsoft.Application.DTOs.ContactoSolicitud;

namespace Airsoft.Application.Interfaces
{
    public interface IContactoSolicitudService
    {
        Task<ApiResponse<bool>> Create(ContactoSolicitudSaveRequest request);
        Task<ApiResponse<bool>> ChangeStatus(ContactoSolicitudChangeStatusRequest request);
        Task<ApiResponse<List<GetSolicitudPendientesResponse>>> GetSolicitudPendientes();

    }
}
