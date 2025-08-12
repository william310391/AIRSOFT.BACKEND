namespace Airsoft.Infrastructure.Intefaces
{
    public interface IUnitOfWork
    {
        IPersonaRepository PersonaRepository { get; }
    }
}
