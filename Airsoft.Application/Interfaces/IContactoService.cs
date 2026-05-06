using Airsoft.Application.DTOs;
using Airsoft.Application.DTOs.Contacto;

namespace Airsoft.Application.Interfaces
{
    public interface IContactoService
    {
        Task<ApiResponse<List<GetContactosByUsuarioIDResponse>>> GetContactos();
        Task<ApiResponse<List<FindContactoByBuscarResponse>>> FindContactoByBuscar(FindContactoByBuscarRequest req);
    }
}
