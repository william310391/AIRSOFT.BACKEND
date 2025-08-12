
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;

namespace Airsoft.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DapperContext _context;

        public IPersonaRepository PersonaRepository { get; }

        public UnitOfWork(DapperContext context, IPersonaRepository personaRepository)
        {
            _context = context;
            PersonaRepository = personaRepository;
        }
    }
}
