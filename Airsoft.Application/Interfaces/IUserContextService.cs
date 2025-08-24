namespace Airsoft.Application.Interfaces
{
    public interface IUserContextService
    {
        string? GetUsuarioID();
        string? GetUsuarioNombre();
        string? GetRol();
        //string? GetAttribute(string attribute);
        T? GetAttribute<T>(string attribute);

    }
}
