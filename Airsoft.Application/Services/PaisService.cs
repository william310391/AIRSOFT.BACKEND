using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Interfaces;
using Airsoft.Infrastructure.Intefaces;
using AutoMapper;

namespace Airsoft.Application.Services
{
    public class PaisService : IPaisService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public PaisService(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<ApiResponse<List<PaisResponse>>> GetPaisAll()
        {
            var lista = await _unitOfWork.PaisRepository.GetPaisAll();
            return new ApiResponse<List<PaisResponse>>
            {
                Success = true,
                Message = "Paises Obtenidos",
                Data = _mapper.Map<List<PaisResponse>>(lista),
            };

        }
    }
}
