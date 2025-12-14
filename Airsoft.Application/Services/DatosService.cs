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

        public async Task<ApiResponse<DatosResponse>> Create(DatosRequest request)
        {
            var usuarioID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            if (usuarioID == 0)
                throw new ApiResponseExceptions(HttpStatusCode.Unauthorized, "No se encontró el usuario en el contexto");

            if (await _unitOfWork.DatosRepository.ExistsDato(request.TipoDato, request.DatoID))
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "El dato ingresado ya existe");

            var datos = _mapper.Map<Datos>(request);
            datos.UsuarioRegistroID = usuarioID;
            var result = await _unitOfWork.DatosRepository.Save(datos);
            if (!result)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "No se pudo crear el dato");

            return new ApiResponse<DatosResponse>
            {
                Success = true,
                Message = "Dato creado exitosamente",
                Data = _mapper.Map<DatosResponse>(datos)
            };
        }

        public async Task<ApiResponse<DatosResponse>> Update(DatosRequest request)
        {
            var usuarioID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            if (usuarioID == 0)
                throw new ApiResponseExceptions(HttpStatusCode.Unauthorized, "No se encontró el usuario en el contexto");

            if (!await _unitOfWork.DatosRepository.ExistsDato(request.TipoDato, request.DatoID))
                throw new ApiResponseExceptions(HttpStatusCode.Conflict, "El dato ingresado no existe");

            var datos = _mapper.Map<Datos>(request);
            datos.UsuarioModificacionID = usuarioID;
            var result = await _unitOfWork.DatosRepository.Update(datos);
            if (!result)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "No se pudo actualizar el dato");
            return new ApiResponse<DatosResponse>
            {
                Success = true,
                Message = "Dato actualizado exitosamente",
                Data = _mapper.Map<DatosResponse>(datos)
            };



        }
    }
}
