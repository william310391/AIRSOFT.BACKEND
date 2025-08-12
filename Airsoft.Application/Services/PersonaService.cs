using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Interfaces;
using Airsoft.Infrastructure.Intefaces;
using AutoMapper;

namespace Airsoft.Application.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<PersonaResponse>>> GetPersonas() {
            var personas = await _unitOfWork.PersonaRepository.GetPersonas();

            return new ApiResponse<IEnumerable<PersonaResponse>>
            {
                Success = true,
                Message = "Persona obtenida correctamente",
                Data = _mapper.Map<IEnumerable<PersonaResponse>>(personas),                
            }; 
        }


        public async Task<ApiResponse<PersonaResponse>> GetPersonaByID(int personaID)
        {
            var persona = await _unitOfWork.PersonaRepository.GetPersonaByID(personaID);

            if (persona == null)
            {
                return new ApiResponse<PersonaResponse>
                {
                    Success = false,
                    Message = $"No se encontró la persona con ID {personaID}",
                    Data = null,
                };
            }

            return new ApiResponse<PersonaResponse>
            {
                Success = true,
                Message = "Persona obtenida correctamente",
                Data = _mapper.Map<PersonaResponse>(persona),
            };
        }

    }
}
