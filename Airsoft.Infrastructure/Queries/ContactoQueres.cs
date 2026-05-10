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
                                            ,IIF(C.UsuarioContactoID IS NULL,1,0) AS NoContacto
                                            ,IIF(@UsuarioID IN (CS.UsuarioID,CS.UsuarioContactoID),1,0) AS SolicitudPendiente
                                            ,IIF(CS.UsuarioID=@UsuarioID,1,0) EsRemitente
                                            ,CS.ContactoSolicitudID
                                            ,CS.UsuarioID AS SolicitudUsuarioID
                                            ,CS.UsuarioContactoID AS SolicitudUsuarioContactoID
                                            ,CM.ChatID
                                        FROM usuario U
                                        LEFT JOIN Chat_Miembro CM ON CM.UsuarioID=U.UsuarioID AND CM.UsuarioID=@UsuarioID
                                        LEFT JOIN Contacto C on C.UsuarioID=@UsuarioID AND C.UsuarioContactoID=U.UsuarioID
                                        LEFT JOIN Contacto_Solicitud CS ON (CS.UsuarioID=U.UsuarioID OR CS.UsuarioContactoID=U.UsuarioID)
                                                              AND CS.Activo=1 
                                                              AND CS.EstadoID=1003
                                        WHERE U.UsuarioID<>@UsuarioID AND U.Activo=1";



        public static readonly string Save = @"INSERT INTO Contacto(UsuarioID,UsuarioContactoID,Activo,UsuarioRegistroID,FechaRegistro)
                                               VALUES(@UsuarioID,@UsuarioContactoID,1,@UsuarioID,GETDATE())";
    }
}
