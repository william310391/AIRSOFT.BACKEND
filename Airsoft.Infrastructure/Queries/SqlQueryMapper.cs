
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
                                    ,U.Activo

                                FROM Usuario U
                                INNER JOIN Rol R ON R.RolID=U.RolID
                                WHERE U.UsuarioCuenta = @UsuarioCuenta" },
            { UsuarioQueries.GetUsuariosByUsuarioID, @"
                                SELECT 
                                     U.UsuarioID
                                    ,U.UsuarioCuenta
                                    ,U.UsuarioNombre
                                    ,U.Contrasena
                                    ,U.FechaCreacion
                                    ,U.RolID
                                    ,R.RolNombre
                                    ,U.Activo

                                FROM Usuario U
                                INNER JOIN Rol R ON R.RolID=U.RolID
                                WHERE U.UsuarioID = @UsuarioID" },
            { UsuarioQueries.GetUsuariosAll,@"
                                SELECT 
                                     U.UsuarioID
                                    ,U.UsuarioCuenta
                                    ,U.UsuarioNombre
                                    ,U.Contrasena
                                    ,U.FechaCreacion
                                    ,U.RolID
                                    ,R.RolNombre
                                    ,U.Activo

                                FROM Usuario U
                                INNER JOIN Rol R ON R.RolID=U.RolID" },

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
                                    ,U.Activo
                                FROM Usuario U
                                INNER JOIN Rol R ON R.RolID = U.RolID
                                WHERE (@Buscar IS NULL OR U.UsuarioCuenta LIKE '%' + @Buscar + '%')
                                ORDER BY U.UsuarioID
                                OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

                                SELECT COUNT(*)
                                FROM Usuario U
                                WHERE (@Buscar IS NULL OR U.UsuarioCuenta LIKE '%' + @Buscar + '%');"
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
            #endregion

            #region Rol
            { RolQueries.GetAllRol, @"SELECT RolID, RolNombre FROM Rol " },

            #endregion


            #region Menu Pagina
            { MenuPaginaQueries.GetMenuPaginasByPersonaID, @"                      
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
                                INNER JOIN Pagina_Rol PR ON PR.RolID=U.RolID AND PR.Activo=1
                                INNER JOIN Pagina P ON P.PaginaID=PR.PaginaID AND P.Activo=1
                                INNER JOIN Menu M ON M.MenuID=P.MenuID AND M.Activo=1
                                WHERE U.UsuarioID = CASE WHEN @RolID=1 THEN U.UsuarioID ELSE @UsuarioID END" }
            #endregion
        };

        public static string Get<T>(T type) where T : Enum
        {
            return queries[type];
        }
    }

}
