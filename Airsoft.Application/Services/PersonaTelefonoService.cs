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
    public class PersonaTelefonoService: IPersonaTelefonoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        public PersonaTelefonoService(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<ApiResponse<List<PersonaTelefonoResponse>>> GetByPersonaID(int personaID)
        {
            var persona = await _unitOfWork.PersonaRepository.GetPersonaByID(personaID);
            if (persona != null)
                throw new ApiResponseExceptions(HttpStatusCode.Unauthorized, "No existe el codigo de personaID");

            var lista = await _unitOfWork.PersonaTelefonoRepository.GetByPersonaID(personaID);
            return new ApiResponse<List<PersonaTelefonoResponse>>
            {
                Success = true,
                Message = "Telefonos Obtenidos",
                Data = _mapper.Map<List<PersonaTelefonoResponse>>(lista)
            };
        }

        public async Task<ApiResponse<PersonaTelefonoResponse>> Save(PersonaTelefonoRequest request)
        {
            var existeTipoCorreo = (await _unitOfWork.DatosRepository.FindByTipoDato("TIPO_TELEFONO")).Any(x => x.DatoID == request.TipoTelefonoID);
            if (existeTipoCorreo)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "No existe el codigo de tipo correo");

            var entidad = _mapper.Map<PersonaTelefono>(request);
            entidad.UsuarioRegistroID= _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            var result = await _unitOfWork.PersonaTelefonoRepository.Save(entidad);

            if(!result)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "No se pudo crear el telefono");

            return new ApiResponse<PersonaTelefonoResponse>
            {
                Success = result,
                Message = result ? "telefono guardado" : "Error al guardar telefono",
                Data = _mapper.Map<PersonaTelefonoResponse>(entidad)
            };
        }

        public async Task<ApiResponse<PersonaTelefonoResponse>> Update(PersonaTelefonoRequest request)
        {
            var existeTipoCorreo = (await _unitOfWork.DatosRepository.FindByTipoDato("TIPO_TELEFONO")).Any(x => x.DatoID == request.TipoTelefonoID);
            if (existeTipoCorreo)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "No existe el codigo de tipo telefono");

            var entidad = _mapper.Map<PersonaTelefono>(request);
            entidad.UsuarioRegistroID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            var result = await _unitOfWork.PersonaTelefonoRepository.Update(entidad);

            if (!result)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "No se pudo actualizar el telefono");

            return new ApiResponse<PersonaTelefonoResponse>
            {
                Success = result,
                Message = result ? "Telefono actualizado" : "Error al actualizar el telefono",
                Data = _mapper.Map<PersonaTelefonoResponse>(entidad)
            };
        }
        public async Task<ApiResponse<bool>> ChangeState(PersonaTelefonoRequest request)
        {
            var dato = await _unitOfWork.PersonaTelefonoRepository.GetByPersonaTelefonoID(request.PersonaTelefonoID);
            if (dato == null)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "El registro no existe");

            var result = await _unitOfWork.PersonaTelefonoRepository.ChangeState(request.PersonaTelefonoID, _userContextService.GetAttribute<int>(EnumClaims.UsuarioID),request.Activo);

            if (!result)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "No se pudo cambiar el estado del telefono");

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Estado del telefono Cambiado",
                Data = result
            };

        }

    }
}
