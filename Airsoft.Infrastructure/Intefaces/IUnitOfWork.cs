namespace Airsoft.Infrastructure.Intefaces
{
    public interface IUnitOfWork
    {
        IPersonaRepository PersonaRepository { get; }
        IUsuarioRepository UsuarioRepository { get; }
        IRolRepository RolRepository { get; }
        IMenuPaginaRepository MenuPaginaRepository { get; }
    }
}
