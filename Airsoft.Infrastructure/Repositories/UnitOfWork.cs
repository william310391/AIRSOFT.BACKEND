
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;

namespace Airsoft.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DapperContext _context;

        public IPersonaRepository PersonaRepository { get; }
        public IUsuarioRepository UsuarioRepository { get; }

        public UnitOfWork(DapperContext context
            , IPersonaRepository personaRepository
            , IUsuarioRepository usuarioRepository)
        {
            _context = context;
            PersonaRepository = personaRepository;
            UsuarioRepository = usuarioRepository;
        }
    }
}
