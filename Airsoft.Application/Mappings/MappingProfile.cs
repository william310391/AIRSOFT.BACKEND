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

            // DTO → Entity
            CreateMap<PersonaRequest, Persona>();
            CreateMap<UsuarioRequest, Usuario>();
            CreateMap<MenuPaginaRequest, MenuPagina>();

            // Si los nombres de propiedades no coinciden:
            // CreateMap<Persona, PersonaResponse>()
            //     .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.Nombre + " " + src.Apellido));
        }
    }
}
