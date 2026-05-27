namespace Airsoft.Infrastructure.Queries
{
    public class ChatQueries
    {
        public static readonly string Save = 
        @"INSERT INTO Chat(ChatID,EsPrivado,nombreChat,Activo,FechaRegistro,UsuarioRegistroID)
          VALUES(@ChatID,@EsPrivado,@nombreChat,1,getdate(),@UsuarioRegistroID)";


        public static readonly string UpdateUnread = @"
                UPDATE Contacto SET MensajeNoleidos=0
                WHERE ChatID=@ChatID
                AND UsuarioID=@UsuarioID
                AND UsuarioContactoID=@UsuarioContactoID";

        public static readonly string AddUnread = @"
                UPDATE Contacto SET MensajeNoleidos+=1
                WHERE ChatID=@ChatID 
                AND UsuarioID<>@UsuarioID";
    }
}
