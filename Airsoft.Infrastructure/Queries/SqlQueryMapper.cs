
namespace Airsoft.Infrastructure.Queries
{
    public static class SqlQueryMapper
    {
        private static readonly Dictionary<object, string> queries = new()
        {
                #region Persona
                { PersonaQueries.GetPersonasById, @"SELECT * FROM Persona WHERE PersonaID = @PersonaID" },
                { PersonaQueries.GetPersonas, @"SELECT * FROM Persona"},
                #endregion

                #region Usuario
                { UsuarioQueries.GetUsuariosByUsuarioNombre, @"
                                    SELECT 
                                     U.UsuarioID
                                    ,U.UsuarioNombre
                                    ,U.Contrasena
                                    ,U.FechaCreacion
                                    ,U.RolID
                                    ,R.RolNombre

                                    FROM Usuario U
                                    INNER JOIN Rol R ON R.RolID=U.RolID
                                    WHERE U.UsuarioNombre = @UsuarioNombre" },

                { UsuarioQueries.ExistsUasuario, @"
                                    SELECT CASE 
                                             WHEN EXISTS (SELECT 1 FROM Usuario WHERE UsuarioNombre = @UsuarioNombre) 
                                             THEN 1 
                                             ELSE 0 
                                           END AS ExisteUsuario;" },

                { UsuarioQueries.SaveUsuario, @"
                                    INSERT INTO Usuario(UsuarioNombre,Contrasena,FechaCreacion,RolID) 
                                    values(@UsuarioNombre,@Contrasena,GETDATE(),@RolID)" },
            #endregion

            #region Rol
                { RolQueries.GetAllRol, @"
                        SELECT RolID, RolNombre FROM Rol " }

            #endregion
        };

    public static string Get<T>(T type) where T : Enum
        {
            return queries[type];
        }
    }

}
