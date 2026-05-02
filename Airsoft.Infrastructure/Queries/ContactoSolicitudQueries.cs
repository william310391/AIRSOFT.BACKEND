namespace Airsoft.Infrastructure.Queries
{
    public class ContactoSolicitudQueries
    {
        public static readonly string Save = @"INSERT INTO Contacto_Solicitud (UsuarioID
                                                               ,UsuarioContactoID
                                                               ,Activo
                                                               ,EstadoID
                                                               ,UsuarioRegistroID
                                                               ,Mensaje)
                                                        VALUES (@UsuarioID
                                                               ,@UsuarioContactoID
                                                               ,1
                                                               ,1003
                                                               ,@UsuarioID
                                                               ,@Mensaje) ";

        public static readonly string changeStatus = @"UPDATE Contacto_Solicitud SET 
                                                         EstadoID=@EstadoID
                                                        ,Activo=0
                                                        ,UsuarioModificacionID = @UsuarioID 
                                                        ,FechaModificacion = GETDATE()
                                                        WHERE 
                                                        UsuarioID=@UsuarioID AND 
                                                        UsuarioContactoID= @UsuarioContactoID AND 
                                                        Activo=1 AND
                                                        EstadoID=1003";

        public static readonly string GetSolicitudPendientesByUsuarioID = @"
                                        select ContactoSolicitudID
                                              ,UsuarioID
                                              ,UsuarioContactoID
                                              ,Mensaje
                                              ,FechaRegistro
                                              ,FechaRegistro
                                        from Contacto_Solicitud
                                        where UsuarioID=@UsuarioID || UsuarioContactoID=@UsuarioID AND Activo=1 AND EstadoID=1003";



    }
}
