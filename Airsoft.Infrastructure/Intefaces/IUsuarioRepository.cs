using Airsoft.Domain.Entities;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUsuariosByUsuarioNombre(string usuarioNombre);
        Task<Usuario> GetUsuariosByUsuarioID(int usuarioID);
        Task<bool> ExistsUsuario(string usuarioNombre);
        Task<bool> SaveUsuario(Usuario usuario);
    }
}