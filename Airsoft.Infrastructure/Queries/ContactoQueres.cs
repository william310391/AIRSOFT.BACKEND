namespace Airsoft.Infrastructure.Queries
{
    public class ContactoQueres
    {
        public static readonly string GetContacto = @"
                             SELECT 
                                  U.UsuarioID
                                 ,C.UsuarioContactoID
                                 ,U.UsuarioNombre AS ContactoNombre
                                 ,U.UsuarioCuenta AS ContactoCorreo
                                 ,IIF(C.UsuarioContactoID IS NULL,1,0) AS NoContacto
                                 ,IIF(U.UsuarioID IN (CS.UsuarioID,CS.UsuarioContactoID),1,0) AS SolicitudPendiente
                                 ,IIF(CS.UsuarioID = @UsuarioID, 1, 0) AS EsRemitente
                                 ,CS.ContactoSolicitudID
                                 ,CS.UsuarioID AS SolicitudUsuarioID
                                 ,CS.UsuarioContactoID AS SolicitudUsuarioContactoID
                                 ,CM.ChatID
                                 ,ISNULL(CM.MensajeNoleidos,0) AS MensajeNoleidos
                                 ,IIF(CH.EsPrivado=1,U.UsuarioNombre,CH.nombreChat) AS nombreChat
                                 ,CH.EsPrivado
                                 ,CS.Mensaje AS SolicitudMensaje
                                 ,CASE 
                                     WHEN C.ContactoID IS NOT NULL THEN 'CONTACTO'
                                     WHEN CS.ContactoSolicitudID IS NOT NULL THEN 'SOLICITUD'
                                     ELSE 'SIN_RELACION'
                                  END AS TipoRegistro
                             FROM Usuario U
                             LEFT JOIN Contacto C 
                                 ON C.UsuarioID = @UsuarioID
                                 AND C.UsuarioContactoID = U.UsuarioID
                                 AND C.Activo = 1
                             LEFT JOIN Chat_Miembro CM 
                                 ON CM.UsuarioID = U.UsuarioID
                                 AND EXISTS (
                                     SELECT 1
                                     FROM Chat_Miembro CM2
                                     WHERE CM2.ChatID = CM.ChatID
                                         AND CM2.UsuarioID = @UsuarioID
                                 )
                             LEFT JOIN Chat CH ON CH.ChatID=CM.ChatID                             
                             LEFT JOIN Contacto_Solicitud CS
                                 ON (
                                     (CS.UsuarioID = @UsuarioID AND CS.UsuarioContactoID = U.UsuarioID)
                                     OR
                                     (CS.UsuarioContactoID = @UsuarioID AND CS.UsuarioID = U.UsuarioID)
                                 )
                                 AND CS.Activo = 1
                                 AND CS.EstadoID = 1003
                             WHERE 
                                 C.ContactoID IS NOT NULL
                                 OR CS.ContactoSolicitudID IS NOT NULL";

        public static readonly string FindContacto = @"
                            SELECT TOP 20
                                 U.UsuarioID
                                ,C.UsuarioContactoID
                                ,U.UsuarioNombre AS ContactoNombre
                                ,U.UsuarioCuenta AS ContactoCorreo
                                ,IIF(C.UsuarioContactoID IS NULL, 1, 0) AS NoContacto
                                ,IIF(CS.ContactoSolicitudID IS NOT NULL, 1, 0) AS SolicitudPendiente
                                ,IIF(CS.UsuarioID = @UsuarioID, 1, 0) AS EsRemitente
                                ,CS.ContactoSolicitudID
                                ,CS.UsuarioID AS SolicitudUsuarioID
                                ,CS.UsuarioContactoID AS SolicitudUsuarioContactoID
                                ,CS.Mensaje AS SolicitudMensaje
                                ,CM.ChatID
                                ,ISNULL(CM.MensajeNoleidos,0) AS MensajeNoleidos
                                ,IIF(CH.EsPrivado=1,U.UsuarioNombre,CH.nombreChat) AS nombreChat
                                ,CH.EsPrivado
                                ,CASE 
                                    WHEN C.ContactoID IS NOT NULL THEN 'CONTACTO'
                                    WHEN CS.ContactoSolicitudID IS NOT NULL THEN 'SOLICITUD'
                                    ELSE 'SIN_RELACION'
                                 END AS TipoRegistro

                            FROM Usuario U
                            -- Contacto existente
                            LEFT JOIN Contacto C 
                                ON C.UsuarioID = @UsuarioID
                                AND C.UsuarioContactoID = U.UsuarioID
                                AND C.Activo = 1
                            -- Solicitud pendiente entre ambos usuarios
                            LEFT JOIN Contacto_Solicitud CS 
                                ON (
                                    (CS.UsuarioID = @UsuarioID AND CS.UsuarioContactoID = U.UsuarioID)
                                    OR
                                    (CS.UsuarioContactoID = @UsuarioID AND CS.UsuarioID = U.UsuarioID)
                                )
                                AND CS.Activo = 1
                                AND CS.EstadoID = 1003
                            -- Chat compartido
                            LEFT JOIN Chat_Miembro CM 
                                ON CM.UsuarioID = U.UsuarioID
                                AND EXISTS (
                                    SELECT 1
                                    FROM Chat_Miembro CM2
                                    WHERE CM2.ChatID = CM.ChatID
                                      AND CM2.UsuarioID = @UsuarioID
                                )
                            LEFT JOIN Chat CH ON CH.ChatID=CM.ChatID
                            WHERE 
                                U.UsuarioID <> @UsuarioID
                                AND U.Activo = 1
                                AND (
                                    U.UsuarioNombre LIKE '%' + @Buscar + '%'
                                    OR U.UsuarioCuenta LIKE '%' + @Buscar + '%'
                                )
                            ORDER BY 
                                U.UsuarioNombre";

        public static readonly string Save = @"INSERT INTO Contacto(UsuarioID,UsuarioContactoID,Activo,UsuarioRegistroID,FechaRegistro)
                                               VALUES(@UsuarioID,@UsuarioContactoID,1,@UsuarioID,GETDATE())";
    }
}
