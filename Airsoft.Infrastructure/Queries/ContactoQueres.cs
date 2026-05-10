namespace Airsoft.Infrastructure.Queries
{
    public class ContactoQueres
    {
        //public static readonly string GetContactosByUsuarioID = @"
        //                            SELECT 
        //                                C.ContactoID
        //                                ,C.UsuarioID
        //                                ,C.UsuarioContactoID
        //                                ,U.UsuarioNombre AS ContactoNombre
        //                                ,U.UsuarioCuenta AS ContactoCorreo
        //                                ,C.Activo
        //                                ,C.UsuarioRegistroID
        //                                ,C.FechaRegistro
        //                                ,C.UsuarioModificacionID
        //                                ,C.FechaModificacion
        //                            FROM Contacto C
        //                            INNER JOIN Usuario U ON C.UsuarioContactoID=U.UsuarioID
        //                            WHERE C.UsuarioID=@UsuarioID AND C.Activo=1";


        public static readonly string GetContactosByUsuarioID = @"
                                                    SELECT 
                                                         C.UsuarioContactoID
                                                        ,U.UsuarioID
                                                        ,U.UsuarioNombre AS ContactoNombre
                                                        ,U.UsuarioCuenta AS ContactoCorreo

                                                        ,IIF(C.UsuarioContactoID IS NULL,1,0) AS NoContacto
                                                        ,IIF(U.UsuarioID IN (CS.UsuarioID,CS.UsuarioContactoID),1,0) AS SolicitudPendiente
                                                        ,IIF(CS.UsuarioID = @UsuarioID, 1, 0) AS EsRemitente
                                                        ,CS.ContactoSolicitudID
                                                        ,CS.UsuarioID AS SolicitudUsuarioID
                                                        ,CS.UsuarioContactoID AS SolicitudUsuarioContactoID
                                                        ,CS.Mensaje AS SolicitudMensaje

                                                        ,CM.ChatID
                                                        ,ISNULL(C.UsuarioRegistroID, CS.UsuarioRegistroID) AS UsuarioRegistroID
                                                        ,ISNULL(C.FechaRegistro, CS.FechaRegistro) AS FechaRegistro
                                                        ,ISNULL(C.UsuarioModificacionID, CS.UsuarioModificacionID) AS UsuarioModificacionID
                                                        ,ISNULL(C.FechaModificacion, CS.FechaModificacion) AS FechaModificacion
                                                        ,CASE 
                                                            WHEN C.ContactoID IS NOT NULL THEN 'CONTACTO'
                                                            WHEN CS.ContactoSolicitudID IS NOT NULL THEN 'SOLICITUD'
                                                         END AS TipoRegistro
                                                    FROM Usuario U
                                                    LEFT JOIN Chat_Miembro CM ON CM.UsuarioID=U.UsuarioID AND CM.UsuarioID=@UsuarioID
                                                    LEFT JOIN Contacto C 
                                                        ON C.UsuarioID = @UsuarioID
                                                        AND C.UsuarioContactoID = U.UsuarioID
                                                        AND C.Activo = 1
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


        //public static readonly string FindContacto = @"
        //                                SELECT TOP 20
        //                                    U.UsuarioID
        //                                    ,U.UsuarioNombre AS ContactoNombre
        //                                    ,U.UsuarioCuenta AS ContactoCorreo
        //                                    ,C.UsuarioContactoID

        //                                    ,IIF(C.UsuarioContactoID IS NULL,1,0) AS NoContacto
        //                                    ,IIF(@UsuarioID IN (CS.UsuarioID,CS.UsuarioContactoID),1,0) AS SolicitudPendiente
        //                                    ,IIF(CS.UsuarioID=@UsuarioID,1,0) EsRemitente
        //                                    ,CS.ContactoSolicitudID
        //                                    ,CS.UsuarioID AS SolicitudUsuarioID
        //                                    ,CS.UsuarioContactoID AS SolicitudUsuarioContactoID
        //                                    ,CS.Mensaje AS SolicitudMensaje

        //                                    ,CM.ChatID
        //                                FROM usuario U
        //                                LEFT JOIN Chat_Miembro CM ON CM.UsuarioID=U.UsuarioID AND CM.UsuarioID=@UsuarioID
        //                                LEFT JOIN Contacto C on C.UsuarioID=@UsuarioID AND C.UsuarioContactoID=U.UsuarioID
        //                                LEFT JOIN Contacto_Solicitud CS ON (CS.UsuarioID=U.UsuarioID OR CS.UsuarioContactoID=U.UsuarioID)
        //                                                      AND CS.Activo=1 
        //                                                      AND CS.EstadoID=1003
        //                                WHERE U.UsuarioID<>@UsuarioID AND U.Activo=1";

        public static readonly string FindContacto = @"

                                        SELECT TOP 20
                                             U.UsuarioID
                                            ,U.UsuarioNombre AS ContactoNombre
                                            ,U.UsuarioCuenta AS ContactoCorreo
                                            ,C.UsuarioContactoID

                                            -- No existe contacto registrado
                                            ,IIF(C.UsuarioContactoID IS NULL, 1, 0) AS NoContacto

                                            -- Existe solicitud pendiente entre ambos usuarios
                                            ,IIF(CS.ContactoSolicitudID IS NOT NULL, 1, 0) AS SolicitudPendiente

                                            -- Usuario actual envió la solicitud
                                            ,IIF(CS.UsuarioID = @UsuarioID, 1, 0) AS EsRemitente

                                            ,CS.ContactoSolicitudID
                                            ,CS.UsuarioID AS SolicitudUsuarioID
                                            ,CS.UsuarioContactoID AS SolicitudUsuarioContactoID
                                            ,CS.Mensaje AS SolicitudMensaje
                                            ,CM.ChatID

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
