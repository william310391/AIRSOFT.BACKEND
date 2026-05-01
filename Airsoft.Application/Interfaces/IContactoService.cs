using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;

namespace Airsoft.Application.Interfaces
{
    public interface IContactoService
    {
        Task<ApiResponse<List<GetContactosByUsuarioIDResponse>>> GetContactos();
        Task<ApiResponse<List<FindContactoByBuscarResponse>>> FindContactoByBuscar(FindContactoByBuscarRequest req);
    }
}
