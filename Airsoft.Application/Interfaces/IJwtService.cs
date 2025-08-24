using Airsoft.Domain.Entities;

namespace Airsoft.Application.Interfaces
{
    public interface IJwtService
    {   
        string GenerarToken(Usuario usuario);
    }
}
