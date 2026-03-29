using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Exceptions;
using Airsoft.Application.Interfaces;
using Airsoft.Domain.Entities;
using Airsoft.Domain.Enum;
using Airsoft.Infrastructure.Intefaces;
using AutoMapper;
using System.Net;

namespace Airsoft.Application.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public PersonaService(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
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
            return new ApiResponse<PersonaResponse>
            {
                Success = persona != null ? true : false,
                Message = persona != null ? "Persona obtenida correctamente" : $"No se encontró la persona con ID {personaID}",
                Data = persona != null ? _mapper.Map<PersonaResponse>(persona) : null,
            };
        }

        public async Task<ApiResponse<bool>> Save(PersonaRequest request)
        {
            await ValidateRequest(request);
            request.UsuarioRegistroID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            var persona = _mapper.Map<Persona>(request);
            var result = await _unitOfWork.PersonaRepository.Save(persona);
            return new ApiResponse<bool>
            {
                Success = result,
                Message = result ? "Persona guardada correctamente" : "Error al guardar la persona",
                Data = result
            };
        }

        public async Task<ApiResponse<bool>> Update(PersonaRequest request)
        {
            await ValidateRequest(request);
            request.UsuarioModeficionID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            var persona = _mapper.Map<Persona>(request);
            var result = await _unitOfWork.PersonaRepository.Update(persona);
            return new ApiResponse<bool>
            {
                Success = result,
                Message = result ? "Persona actualizada correctamente" : "Error al actualizar la persona",
                Data = result
            };

        }
        private async Task ValidateRequest(PersonaRequest request)
        {
            // Validar Tipo de Documento
            var existeTipoDocumento = (await _unitOfWork.DatosRepository
                .FindByTipoDato("TIPO_DOCUMENTO")).Any(x => x.DatoID == request.TipoDocumentoID);

            if (!existeTipoDocumento)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "No existe el tipo de documento");

            // Validar Sexo
            var existeSexo = (await _unitOfWork.DatosRepository
                .FindByTipoDato("TIPO_GENERO")).Any(x => x.DatoID == request.SexoID);

            if (!existeSexo)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "No existe el tipo de género");

            //falta validar pais
        }
    }
}
