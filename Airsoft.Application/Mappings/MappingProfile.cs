using AutoMapper;
using Airsoft.Domain.Entities;
using Airsoft.Application.DTOs.Persona;
using Airsoft.Application.DTOs.PersonaCorreo;
using Airsoft.Application.DTOs.PersonaTelefono;
using Airsoft.Application.DTOs.Ubigeo;
using Airsoft.Application.DTOs.Usuario;
using Airsoft.Application.DTOs.Contacto;
using Airsoft.Application.DTOs.ContactoSolicitud;
using Airsoft.Application.DTOs.Datos;
using Airsoft.Application.DTOs.Pais;

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
            CreateMap<Contacto, GetContactosByUsuarioIDResponse>();
            CreateMap<Contacto, FindContactoByBuscarResponse>();
            CreateMap<ContactoSolicitud, GetSolicitudPendientesResponse>();


            // DTO → Entity
            CreateMap<PersonaRequest, Persona>();
            CreateMap<UsuarioRequest, Usuario>();
            CreateMap<UsuarioDeleteRequest, Usuario>();
            CreateMap<UsuarioChangeStateRequest, Usuario>();
            CreateMap<MenuPaginaRequest, MenuPagina>();
            CreateMap<DatosRequest, Datos>();
            CreateMap<PersonaCorreoRequest, PersonaCorreo>();
            CreateMap<PersonaTelefonoRequest, PersonaTelefono>();
            CreateMap<ContactoSolicitudSaveRequest, ContactoSolicitud>();
            CreateMap<ContactoSolicitudChangeStatusRequest, ContactoSolicitud>();
            

            // Si los nombres de propiedades no coinciden:
            // CreateMap<Persona, PersonaResponse>()
            //     .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.Nombre + " " + src.Apellido));
        }
    }
}
