using AutoMapper;
using Airsoft.Domain.Entities;
using Airsoft.Application.DTOs.Response;
using Airsoft.Application.DTOs.Request;

namespace Airsoft.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entity → DTO
            CreateMap<Persona, PersonaResponse>();
            CreateMap<Usuario, UsuarioResponse>(); 
            CreateMap<MenuPagina, MenuPaginaResponse>();
            CreateMap<Rol, RolResponse>();
            CreateMap<Datos, DatosResponse>();

            // DTO → Entity
            CreateMap<PersonaRequest, Persona>();
            CreateMap<UsuarioRequest, Usuario>();
            CreateMap<UsuarioDeleteRequest, Usuario>();
            CreateMap<UsuarioChangeStateRequest, Usuario>();
            CreateMap<MenuPaginaRequest, MenuPagina>();
            CreateMap<DatosRequest, Datos>();

            // Si los nombres de propiedades no coinciden:
            // CreateMap<Persona, PersonaResponse>()
            //     .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.Nombre + " " + src.Apellido));
        }
    }
}
