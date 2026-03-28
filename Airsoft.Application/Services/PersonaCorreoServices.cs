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
    public class PersonaCorreoServices : IPersonaCorreoServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public PersonaCorreoServices(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<ApiResponse<List<PersonaCorreoResponse>>> GetByPersonaID(int personaID)
        {
            var persona = await _unitOfWork.PersonaRepository.GetPersonaByID(personaID);
            if (persona != null)
                throw new ApiResponseExceptions(HttpStatusCode.Unauthorized, "No existe el codigo de personaID");

            var lista = await _unitOfWork.PersonaCorreoRepository.GetByPersonaID(personaID);
            return new ApiResponse<List<PersonaCorreoResponse>>
            {
                Success = true,
                Message = "Correos Obtenidos",
                Data = _mapper.Map<List<PersonaCorreoResponse>>(lista),
            };
        }

        public async Task<ApiResponse<PersonaCorreoResponse>> Save(PersonaCorreoRequest request)
        {

            var usuarioID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            if (usuarioID == 0)
                throw new ApiResponseExceptions(HttpStatusCode.Unauthorized, "No se encontró el usuario en el contexto");

            var existeTipoCorreo = (await _unitOfWork.DatosRepository.FindByTipoDato("TIPO_CORREO")).Any(x => x.DatoID == request.TipoCorreoID);
            if (existeTipoCorreo)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "No existe el codigo de tipo correo");

            request.UsuarioRegistroID = usuarioID;
            var entidad = _mapper.Map<PersonaCorreo>(request);
            var result = await _unitOfWork.PersonaCorreoRepository.Save(entidad);

            if (!result)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "No se pudo crear el correo");

            return new ApiResponse<PersonaCorreoResponse>
            {
                Success = result,
                Message = result ? "Correo Guardado" : "Error al Guardar Correo",
                Data = _mapper.Map<PersonaCorreoResponse>(entidad)
            };
        }
        public async Task<ApiResponse<PersonaCorreoResponse>> Update(PersonaCorreoRequest request)
        {
            var usuarioID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            if (usuarioID == 0)
                throw new ApiResponseExceptions(HttpStatusCode.Unauthorized, "No se encontró el usuario en el contexto");

            var existeTipoCorreo = (await _unitOfWork.DatosRepository.FindByTipoDato("TIPO_CORREO")).Any(x => x.DatoID == request.TipoCorreoID);
            if (existeTipoCorreo)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "No existe el codigo de tipo correo");

            var entidad = _mapper.Map<PersonaCorreo>(request);
            entidad.UsuarioRegistroID = usuarioID;
            var result = await _unitOfWork.PersonaCorreoRepository.Update(entidad);
            if (!result)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "No se pudo actualizar el correo");

            return new ApiResponse<PersonaCorreoResponse>
            {
                Success = result,
                Message = result ? "Correo Actualizado" : "Error al actualizar Correo",
                Data = _mapper.Map<PersonaCorreoResponse>(entidad)
            };
        }

        public async Task<ApiResponse<bool>> ChangeState(PersonaCorreoRequest request)
        {
            var usuarioID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            if (usuarioID == 0)
                throw new ApiResponseExceptions(HttpStatusCode.Unauthorized, "No se encontró el usuario en el contexto");

            var dato = await _unitOfWork.PersonaCorreoRepository.GetByPersonaCorreoID(request.PersonaCorreoID);
            if (dato == null)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "El registro no existe");

            request.UsuarioRegistroID = usuarioID;
            var result = await _unitOfWork.PersonaCorreoRepository.ChangeState(request.PersonaCorreoID, request.Activo);

            if (!result)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "No se pudo cambiar el estado del correo");

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Estado del Correo Cambiado",
                Data = result
            };
        }
    }
}
