using Airsoft.Application.DTOs.Request;
using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Exceptions;
using Airsoft.Application.Interfaces;
using Airsoft.Infrastructure.Intefaces;
using AutoMapper;
using System.Net;

namespace Airsoft.Application.Services
{
    public class MenuPaginaService: IMenuPaginaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MenuPaginaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<MenuPaginaResponse>>> GetMenuPaginasByPersonaID(MenuPaginaRequest request)
        {
            var lista = await _unitOfWork.MenuPaginaRepository.GetMenuPaginasByPersonaID(request.personaID, request.rolID);

            return new ApiResponse<List<MenuPaginaResponse>>
            {
                Success = true,
                Message = "Persona obtenida correctamente",
                Data = _mapper.Map<List<MenuPaginaResponse>>(lista),
            };
        }

        public async Task<ApiResponse<ObtenerAccesosResponse>> ObtenerAccesos(ObtenerAccesosRequest request)
        {
            var usuario = await _unitOfWork.UsuarioRepository.GetUsuariosByUsuarioID(request.usuarioID);

            if (usuario == null)
                throw new ApiResponseExceptions(HttpStatusCode.BadRequest, "El usuario ingresado existe");

            var listaAccesos = await _unitOfWork.MenuPaginaRepository.GetMenuPaginasByPersonaID(usuario.UsuarioID, usuario.RolID);

            ObtenerAccesosResponse res = new ObtenerAccesosResponse()
            {
                nombreRol = usuario.RolNombre,
                nombreUsuario = usuario.UsuarioNombre,
                listaPagina = _mapper.Map<List<MenuPaginaResponse>>(listaAccesos),
            };

            return new ApiResponse<ObtenerAccesosResponse>
            {
                Success = true,
                Message = "Persona obtenida correctamente",
                Data = res,
            };
        }

    }
}
