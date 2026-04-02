using AutoMapper;
using Airsoft.Domain.Entities;
using Airsoft.Application.DTOs.Response;
using Airsoft.Application.DTOs.Request;
using Airsoft.Application.Services;

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
            CreateMap<PersonaCorreo, PersonaCorreoResponse>();
            CreateMap<PersonaTelefono, PersonaTelefonoResponse>();
            CreateMap<Pais, PaisResponse>();
            CreateMap<Ubigeo, UbigeoResponse>();


            // DTO → Entity
            CreateMap<PersonaRequest, Persona>();
            CreateMap<UsuarioRequest, Usuario>();
            CreateMap<UsuarioDeleteRequest, Usuario>();
            CreateMap<UsuarioChangeStateRequest, Usuario>();
            CreateMap<MenuPaginaRequest, MenuPagina>();
            CreateMap<DatosRequest, Datos>();
            CreateMap<PersonaCorreoRequest, PersonaCorreo>();
            CreateMap<PersonaTelefonoRequest, PersonaTelefono>();
            

            // Si los nombres de propiedades no coinciden:
            // CreateMap<Persona, PersonaResponse>()
            //     .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.Nombre + " " + src.Apellido));
        }
    }
}
