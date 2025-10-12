using Airsoft.Domain.Entities;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUsuarioByUsuarioCuenta(string usuarioCuenta);
        Task<Usuario> GetUsuariosByUsuarioID(int usuarioID);
        Task<List<Usuario>> GetUsuariosAll();
        Task<(List<Usuario> Usuarios, int TotalRegistros)> GetUsuarioFind(string? buscar, int pagina, int tamañoPagina);
        Task<bool> ExistsUsuario(string usuarioCuenta);
        Task<bool> SaveUsuario(Usuario usuario);
        Task<bool> UpdateUsuario(Usuario usuario);
        Task<bool> DeleteUsuario(int usuarioID);
        Task<bool> ChangeState(int usuarioID, bool estado);
    }
}