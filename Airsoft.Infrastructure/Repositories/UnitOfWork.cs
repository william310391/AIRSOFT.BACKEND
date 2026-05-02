
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;

namespace Airsoft.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DapperContext _context;
        public IPersonaRepository PersonaRepository { get; }
        public IUsuarioRepository UsuarioRepository { get; }
        public IRolRepository RolRepository { get; }
        public IMenuPaginaRepository MenuPaginaRepository { get; }
        public IDatosRepository DatosRepository { get; }
        public IPersonaCorreoRepository PersonaCorreoRepository { get; }
        public IPersonaTelefonoRepository PersonaTelefonoRepository { get; }
        public IPaisRepository PaisRepository { get; }
        public IUbigeoRepository UbigeoRepository { get; }
        public IContactoRepository ContactoRepository { get; }
        public IContactoSolicitudRepository ContactoSolicitudRepository { get; }
        public IChatRepository ChatRepository { get; }
        public IChatMiembroRepository ChatMiembroRepository { get; }

        public UnitOfWork(DapperContext context
            , IPersonaRepository personaRepository
            , IUsuarioRepository usuarioRepository
            , IRolRepository rolRepository
            , IMenuPaginaRepository menuPaginaRepository
            , IDatosRepository datosRepository
            , IPersonaCorreoRepository personaCorreoRepository
            , IPersonaTelefonoRepository personaTelefonoRepository
            , IPaisRepository paisRepository
            , IUbigeoRepository ubigeoRepository
            , IContactoRepository contactoRepository
            , IContactoSolicitudRepository contactoSolicitudRepository
            , IChatRepository chatRepository
            , IChatMiembroRepository chatMiembroRepository)
        {
            _context = context;
            PersonaRepository = personaRepository;
            UsuarioRepository = usuarioRepository;
            RolRepository = rolRepository;
            MenuPaginaRepository = menuPaginaRepository;
            DatosRepository = datosRepository;
            PersonaCorreoRepository = personaCorreoRepository;
            PersonaTelefonoRepository = personaTelefonoRepository;
            PaisRepository = paisRepository;
            UbigeoRepository = ubigeoRepository;
            ContactoRepository = contactoRepository;
            ContactoSolicitudRepository = contactoSolicitudRepository;
            ChatRepository = chatRepository;
            ChatMiembroRepository = chatMiembroRepository;

        }
    }
}
