using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;

namespace Airsoft.Application.Interfaces
{
    public interface  IDatosService
    {
        Task<ApiResponse<FindResponse<DatosResponse>>> FindBuscarDato(FindRequest request);
        Task<ApiResponse<List<DatosResponse>>> FindByTipoDato(string tipoDato);
        Task<ApiResponse<DatosResponse>> Create(DatosRequest request);
        Task<ApiResponse<DatosResponse>> Update(DatosRequest request);
    }
}
