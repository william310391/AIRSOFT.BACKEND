
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

        public UnitOfWork(DapperContext context
            , IPersonaRepository personaRepository
            , IUsuarioRepository usuarioRepository
            , IRolRepository rolRepository
            , IMenuPaginaRepository menuPaginaRepository
            , IDatosRepository datosRepository
            , IPersonaCorreoRepository personaCorreoRepository)
        {
            _context = context;
            PersonaRepository = personaRepository;
            UsuarioRepository = usuarioRepository;
            RolRepository = rolRepository;
            MenuPaginaRepository = menuPaginaRepository;
            DatosRepository = datosRepository;
            PersonaCorreoRepository = personaCorreoRepository;
        }
    }
}
