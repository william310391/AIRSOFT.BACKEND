using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Interfaces;
using Airsoft.Infrastructure.Intefaces;
using AutoMapper;

namespace Airsoft.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsuarioService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<List<UsuarioResponse>>> GetUsuarioAll() {
            var lista = await _unitOfWork.UsuarioRepository.GetUsuariosAll();
            return new ApiResponse<List<UsuarioResponse>>
            {
                Success = true,
                Message = "Datos Obtenidos",
                Data = _mapper.Map<List<UsuarioResponse>>(lista),
            };
        }
        public async Task<ApiResponse<FindResponse<UsuarioResponse>>> GetUsuarioFind(FindRequest request)
        {
            var (usuarios, totalRegistros) = await _unitOfWork.UsuarioRepository.GetUsuarioFind(request.buscar,request.pagina, request.tamanoPagina);
            var paginacionResponse = new FindResponse<UsuarioResponse>
            {
                datos = _mapper.Map<List<UsuarioResponse>>(usuarios),
                pagina = request.pagina,
                tamanoPagina = request.tamanoPagina,
                totalRegistros = totalRegistros
            };
            return new ApiResponse<FindResponse<UsuarioResponse>>
            {
                Success = true,
                Message = "Datos Obtenidos",
                Data = paginacionResponse
            };
        }


    }
}
