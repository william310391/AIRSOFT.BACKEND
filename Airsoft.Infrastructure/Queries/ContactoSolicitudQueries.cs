namespace Airsoft.Infrastructure.Queries
{
    public class ContactoSolicitudQueries
    {
        public static readonly string GetSolicitudPendientesByUsuarioID = @"SELECT 
                                                 CS.ContactoSolicitudID
                                                ,CS.UsuarioID
                                                ,CS.UsuarioContactoID AS UsuarioContactoID
                                                ,U.UsuarioNombre AS ContactoNombre
                                                ,U.UsuarioCuenta AS ContactoCorreo
                                                ,CS.Activo
                                                ,CS.UsuarioRegistroID
                                                ,CS.FechaRegistro
                                                ,CS.UsuarioModificacionID
                                                ,CS.FechaModificacion
                                                ,IIF(CS.UsuarioID=@UsuarioID,1,0) AS EsRemitente
                                                ,CS.Mensaje
                                            FROM Contacto_Solicitud CS 
                                            INNER JOIN Usuario U ON (CS.UsuarioID=@UsuarioID OR CS.UsuarioContactoID=@UsuarioID) 
                                                                AND CS.Activo=1 
                                                                AND CS.EstadoID=1003
                                            WHERE IIF(CS.UsuarioID=@UsuarioID,CS.UsuarioContactoID, CS.UsuarioID)= U.UsuarioID";

        public static readonly string Save = @"INSERT INTO Contacto_Solicitud (
                                                                ContactoSolicitudID
                                                               ,UsuarioID
                                                               ,UsuarioContactoID
                                                               ,Activo
                                                               ,EstadoID
                                                               ,UsuarioRegistroID
                                                               ,Mensaje)
                                                        VALUES (@ContactoSolicitudID
                                                               ,@UsuarioID
                                                               ,@UsuarioContactoID
                                                               ,1
                                                               ,1003
                                                               ,@UsuarioID
                                                               ,@Mensaje) ";

        public static readonly string changeStatusByUsuarioID = @"UPDATE Contacto_Solicitud SET 
                                                         EstadoID=@EstadoID
                                                        ,Activo=0
                                                        ,UsuarioModificacionID = @UsuarioID 
                                                        ,FechaModificacion = GETDATE()
                                                        WHERE 
                                                        UsuarioID=@UsuarioID AND 
                                                        UsuarioContactoID= @UsuarioContactoID AND 
                                                        Activo=1 AND
                                                        EstadoID=1003";

        public static readonly string changeStatusByContactoSolicitudID = @"UPDATE Contacto_Solicitud SET 
                                                         EstadoID=@EstadoID
                                                        ,Activo=0
                                                        ,UsuarioModificacionID = @UsuarioID 
                                                        ,FechaModificacion = GETDATE()
                                                        WHERE 
                                                        ContactoSolicitudID=@ContactoSolicitudID AND
                                                        Activo=1 AND
                                                        EstadoID=1003";


    }
}
