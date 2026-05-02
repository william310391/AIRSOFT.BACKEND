using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;

namespace Airsoft.Application.Interfaces
{
    public interface IContactoSolicitudService
    {
        Task<ApiResponse<bool>> Save(ContactoSolicitudSaveRequest request);
        Task<ApiResponse<bool>> ChangeStatus(ContactoSolicitudChangeStatusRequest request);

    }
}
