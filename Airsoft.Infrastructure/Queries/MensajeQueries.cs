namespace Airsoft.Infrastructure.Queries
{
    public class MensajeQueries
    {
        public static readonly string GetMensaje = @"
                                    SELECT  M.ChatID,
                                            M.MensajeID,
                                            M.Contenido,
                                            M.UsuarioEnvioID,
                                            ISNULL(M.FechaEdicion,M.FechaCreacion) as Fecha
                                    FROM Mensaje M
                                    WHERE M.ChatID = @ChatID AND Activo=1
                                    ORDER BY ISNULL(M.FechaEdicion,M.FechaCreacion) DESC   -- Importante para paginación consistente
                                    OFFSET (@PageNumber - 1) * @PageSize ROWS
                                    FETCH NEXT @PageSize ROWS ONLY;";

        public static readonly string Save = @"
                                    INSERT INTO Mensaje(MensajeID,ChatID,Contenido,UsuarioEnvioID,FechaCreacion,Activo)
                                    VALUES(@MensajeID,@ChatID,@Contenido,@UsuarioEnvioID,@Fecha,1)";

        public static readonly string Update = @"
                                    UPDATE Mensaje 
                                    SET Contenido=@Contenido 
                                       ,FechaEdicion=@Fecha
                                    WHERE MensajeID=@MensajeID";


        public static readonly string Delete = @"
                                    UPDATE Mensaje 
                                    SET FechaEliminacion=@Fecha
                                       ,Activo=0
                                    WHERE MensajeID=@MensajeID";

    }
}
