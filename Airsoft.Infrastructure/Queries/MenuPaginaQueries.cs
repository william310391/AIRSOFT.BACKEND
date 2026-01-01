namespace Airsoft.Infrastructure.Queries
{
    public static class MenuPaginaQueries
    {
        public static readonly string GetMenuPaginasByPersonaID = @"
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
                                END";
    }
}
