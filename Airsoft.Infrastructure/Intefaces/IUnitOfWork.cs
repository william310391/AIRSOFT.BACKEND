namespace Airsoft.Infrastructure.Intefaces
{
    public interface IUnitOfWork
    {
        IPersonaRepository PersonaRepository { get; }
        IUsuarioRepository UsuarioRepository { get; }
        IRolRepository RolRepository { get; }
        IMenuPaginaRepository MenuPaginaRepository { get; }
        IDatosRepository DatosRepository { get; }
        IPersonaCorreoRepository PersonaCorreoRepository { get; }
        IPersonaTelefonoRepository PersonaTelefonoRepository { get; }
        IPaisRepository PaisRepository { get; }
        IUbigeoRepository UbigeoRepository { get; }
    }
}
