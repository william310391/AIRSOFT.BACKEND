
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
                                    ,U.UsuarioCuenta
                                    ,U.UsuarioNombre
                                    ,U.Contrasena
                                    ,U.FechaCreacion
                                    ,U.RolID
                                    ,R.RolNombre
                                    ,U.Estado

                                FROM Usuario U
                                INNER JOIN Rol R ON R.RolID=U.RolID
                                WHERE U.UsuarioCuenta = @UsuarioCuenta
                                  AND U.Activo= 1"},
            { UsuarioQueries.GetUsuariosByUsuarioID, @"
                                SELECT 
                                     U.UsuarioID
                                    ,U.UsuarioCuenta
                                    ,U.UsuarioNombre
                                    ,U.Contrasena
                                    ,U.FechaCreacion
                                    ,U.RolID
                                    ,R.RolNombre
                                    ,U.Estado

                                FROM Usuario U
                                INNER JOIN Rol R ON R.RolID=U.RolID
                                WHERE U.UsuarioID = @UsuarioID
                                  AND U.Activo= 1" },
            { UsuarioQueries.GetUsuariosAll,@"
                                SELECT 
                                     U.UsuarioID
                                    ,U.UsuarioCuenta
                                    ,U.UsuarioNombre
                                    ,U.Contrasena
                                    ,U.FechaCreacion
                                    ,U.RolID
                                    ,R.RolNombre
                                    ,U.Estado

                                FROM Usuario U
                                INNER JOIN Rol R ON R.RolID=U.RolID
                                WHERE U.Activo= 1" },

            {
                UsuarioQueries.GetUsuariosFind, @"
                                SELECT 
                                     U.UsuarioID
                                    ,U.UsuarioCuenta
                                    ,U.UsuarioNombre
                                    ,U.Contrasena
                                    ,U.FechaCreacion
                                    ,U.RolID
                                    ,R.RolNombre
                                    ,U.Estado
                                FROM Usuario U
                                INNER JOIN Rol R ON R.RolID = U.RolID
                                WHERE (@Buscar IS NULL OR U.UsuarioCuenta LIKE '%' + @Buscar + '%')
                                  AND U.Activo= 1
                                ORDER BY U.UsuarioID
                                OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

                                SELECT COUNT(*)
                                FROM Usuario U
                                WHERE (@Buscar IS NULL OR U.UsuarioCuenta LIKE '%' + @Buscar + '%')
                                 AND U.Activo= 1;"
            },

            { UsuarioQueries.ExistsUasuario, @"
                                SELECT CASE 
                                            WHEN EXISTS (SELECT 1 FROM Usuario WHERE UsuarioCuenta = @UsuarioCuenta) 
                                            THEN 1 
                                            ELSE 0 
                                        END AS ExisteUsuario;" },

            { UsuarioQueries.SaveUsuario, @"
                                INSERT INTO Usuario(UsuarioCuenta,UsuarioNombre,Contrasena,FechaCreacion,RolID) 
                                values(@UsuarioCuenta,@UsuarioNombre,@Contrasena,GETDATE(),@RolID)" },
            { UsuarioQueries.UpdateUsuario,@"
                                UPDATE Usuario 
                                       SET UsuarioCuenta= @UsuarioCuenta,
                                           UsuarioNombre= @UsuarioNombre,                                   
                                           RolID        = @RolID
                                WHERE UsuarioID= @UsuarioID"},
            { UsuarioQueries.DeleteUsuario,@"
                                UPDATE USUARIO SET Activo=0 WHERE UsuarioID=@UsuarioID"},
            { UsuarioQueries.ChangeState, @"
                                UPDATE USUARIO SET Estado=@Estado WHERE UsuarioID=@UsuarioID"},
         
            #endregion

            #region Rol
            { RolQueries.GetAllRol, @"SELECT RolID, RolNombre FROM Rol " },

            #endregion

            #region Menu Pagina
            { MenuPaginaQueries.GetMenuPaginasByPersonaID, @"
                                DECLARE @RolAdministrador INT = 1
                                IF EXISTS (SELECT 1 FROM Usuario WHERE UsuarioID = @UsuarioID AND RolID = @RolAdministrador AND Activo= 1)
                                BEGIN
                                    SELECT DISTINCT
                                        M.MenuID,
                                        P.PaginaID,
                                        M.MenuNombre,
                                        M.MenuIcono,
                                        M.MenuUrlLink,
                                        P.PaginaNombre,
                                        P.PaginaIcono,
                                        P.PaginaUrlLink
                                    FROM Pagina P
                                    INNER JOIN Menu M ON M.MenuID = P.MenuID AND M.Activo = 1
                                    WHERE P.Activo = 1
                                    ORDER BY M.MenuNombre, P.PaginaNombre
                                END
                                ELSE
                                BEGIN
                                    SELECT 
                                        M.MenuID,
                                        P.PaginaID,
                                        M.MenuNombre,
                                        M.MenuIcono,
                                        M.MenuUrlLink,
                                        P.PaginaNombre,
                                        P.PaginaIcono,
                                        P.PaginaUrlLink
                                    FROM Usuario U
                                    INNER JOIN Pagina_Rol PR ON PR.RolID = U.RolID AND PR.Activo = 1
                                    INNER JOIN Pagina P ON P.PaginaID = PR.PaginaID AND P.Activo = 1
                                    INNER JOIN Menu M ON M.MenuID = P.MenuID AND M.Activo = 1
                                    WHERE U.UsuarioID = @UsuarioID
                                      AND U.Activo= 1
                                    ORDER BY M.MenuNombre, P.PaginaNombre
                                END" }
            #endregion
        };

        public static string Get<T>(T type) where T : Enum
        {
            return queries[type];
        }
    }

}
