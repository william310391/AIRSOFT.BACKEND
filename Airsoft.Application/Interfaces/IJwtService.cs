namespace Airsoft.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerarToken(string usuarioId, string rol);
        string GenerarToken(string usuario);
    }
}
