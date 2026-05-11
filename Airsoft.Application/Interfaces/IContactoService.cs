using Airsoft.Application.DTOs;
using Airsoft.Application.DTOs.Contacto;

namespace Airsoft.Application.Interfaces
{
    public interface IContactoService
    {
        Task<ApiResponse<List<ContatoDetalleResponse>>> GetContactos();
        Task<ApiResponse<List<ContatoDetalleResponse>>> FindContactoByBuscar(FindContactoByBuscarRequest req);
    }
}
