namespace Airsoft.Infrastructure.Queries
{
    public class ContactoQueres
    {    
            public static readonly string GetContactosByUsuarioID = @"
                                    SELECT 
                                        C.ContactoID
                                        ,C.UsuarioID
                                        ,C.UsuarioContactoID
                                        ,U.UsuarioNombre AS ContactoNombre
                                        ,U.UsuarioCuenta AS ContactoCorreo
                                        ,C.Activo
                                        ,C.UsuarioRegistroID
                                        ,C.FechaRegistro
                                        ,C.UsuarioModificacionID
                                        ,C.FechaModificacion
                                    FROM Contacto C
                                    INNER JOIN Usuario U ON C.UsuarioContactoID=U.UsuarioID
                                    WHERE C.UsuarioID=@UsuarioID AND C.Activo=1";

        public static readonly string FindContacto = @"
                                    SELECT TOP 20
                                        U.UsuarioID
                                        ,U.UsuarioNombre AS ContactoNombre
                                        ,U.UsuarioCuenta AS ContactoCorreo
                                        ,C.UsuarioContactoID
                                    FROM usuario U
                                    LEFT JOIN Contacto C on C.UsuarioID=U.UsuarioID 
                                    WHERE U.UsuarioID<>@UsuarioID
                                    AND (U.UsuarioNombre LIKE '%'+@Buscar+'%' OR 
                                         U.UsuarioCuenta LIKE '%'+@Buscar+'%')
                                    AND U.Activo=1";

        public static readonly string Save = @"INSERT INTO Contacto(UsuarioID,UsuarioContactoID,Activo,UsuarioRegistroID,FechaRegistro)
                                               VALUES(@UsuarioID,@UsuarioContactoID,1,@UsuarioID,GETDATE())";
    }
}
