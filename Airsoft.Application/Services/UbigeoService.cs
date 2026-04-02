using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Interfaces;
using Airsoft.Infrastructure.Intefaces;
using AutoMapper;

namespace Airsoft.Application.Services
{
    public class UbigeoService : IUbigeoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public UbigeoService(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<ApiResponse<List<UbigeoResponse>>> GetDepartamentos()
        {
            var lista = await _unitOfWork.UbigeoRepository.GetDepartamentos();
            return new ApiResponse<List<UbigeoResponse>>
            {
                Success = true,
                Message = "Datos Obtenidos",
                Data = _mapper.Map<List<UbigeoResponse>>(lista),
            };
        }
        public async Task<ApiResponse<List<UbigeoResponse>>> GetProvincias(int departamentoID)
        {
            var lista = await _unitOfWork.UbigeoRepository.GetProvincias(departamentoID);
            return new ApiResponse<List<UbigeoResponse>>
            {
                Success = true,
                Message = "Datos Obtenidos",
                Data = _mapper.Map<List<UbigeoResponse>>(lista),
            };
        }

        public async Task<ApiResponse<List<UbigeoResponse>>> GetDistritos(int departamentoID, int provinciaID)
        {
            var lista = await _unitOfWork.UbigeoRepository.GetDistritos(departamentoID, provinciaID);
            return new ApiResponse<List<UbigeoResponse>>
            {
                Success = true,
                Message = "Datos Obtenidos",
                Data = _mapper.Map<List<UbigeoResponse>>(lista),
            };
        }
    }
}
