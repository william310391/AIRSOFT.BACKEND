using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Interfaces;
using Airsoft.Infrastructure.Intefaces;
using AutoMapper;

namespace Airsoft.Application.Services
{
    public class DatosService : IDatosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public DatosService(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<ApiResponse<FindResponse<DatosResponse>>> FindBuscarDato(FindRequest request)
        {
            var (datos, totalRegistros) = await _unitOfWork.DatosRepository.FindBuscarDato(request.buscar, request.pagina, request.tamanoPagina);

            var paginacionResponse = new FindResponse<DatosResponse>
            {
                datos = _mapper.Map<List<DatosResponse>>(datos),
                pagina = request.pagina,
                tamanoPagina = request.tamanoPagina,
                totalRegistros = totalRegistros
            };

            return new ApiResponse<FindResponse<DatosResponse>>
            {
                Success = true,
                Message = "Datos Obtenidos",
                Data = paginacionResponse
            };

        }

        public async Task<ApiResponse<List<DatosResponse>>> FindByTipoDato(string tipoDato)
        {
            var lista = await _unitOfWork.DatosRepository.FindByTipoDato(tipoDato);
            return new ApiResponse<List<DatosResponse>>
            {
                Success = true,
                Message = "Datos Obtenidos",
                Data = _mapper.Map<List<DatosResponse>>(lista),
            };
        }



    }
}
