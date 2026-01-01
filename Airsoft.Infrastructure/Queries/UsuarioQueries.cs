namespace Airsoft.Infrastructure.Queries
{
    public static class UsuarioQueries
    {
        public static readonly string GetUsuariosByUsuarioNombre = @"
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
                                    AND U.Activo= 1";
        public static readonly string GetUsuariosByUsuarioID = @"
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
                                  AND U.Activo= 1";
        public static readonly string GetUsuariosAll = @"
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
                                WHERE U.Activo= 1";
        public static readonly string GetUsuariosFind = @"
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
                                 AND U.Activo= 1;";
        public static readonly string ExistsUasuario = @"
                                SELECT CASE 
                                    WHEN EXISTS (SELECT 1 FROM Usuario WHERE UsuarioCuenta = @UsuarioCuenta) 
                                    THEN 1 
                                    ELSE 0 
                                END AS ExisteUsuario;";
        public static readonly string SaveUsuario = @"
                                INSERT INTO Usuario(UsuarioCuenta,UsuarioNombre,Contrasena,FechaCreacion,RolID) 
                                values(@UsuarioCuenta,@UsuarioNombre,@Contrasena,GETDATE(),@RolID)";
        public static readonly string UpdateUsuario = @"
                                UPDATE Usuario 
                                       SET UsuarioCuenta= @UsuarioCuenta,
                                           UsuarioNombre= @UsuarioNombre,                                   
                                           RolID        = @RolID
                                WHERE UsuarioID= @UsuarioID";
        public static readonly string DeleteUsuario = @"
                                UPDATE USUARIO SET Activo=0 WHERE UsuarioID=@UsuarioID";
        public static readonly string ChangeState = @"
                                UPDATE USUARIO SET Estado=@Estado WHERE UsuarioID=@UsuarioID";
    }
}
