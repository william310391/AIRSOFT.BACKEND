using Airsoft.Domain.Entities;

namespace Airsoft.Infrastructure.Intefaces
{
    public interface IMenuPaginaRepository
    {
        Task<List<MenuPagina>> GetMenuPaginasByPersonaID(int usuarioID, int rolID);
    }
}
