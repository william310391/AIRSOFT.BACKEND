using Airsoft.Domain.Entities;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUsuariosByUsuarioNombre(string usuarioNombre);
    }
}
