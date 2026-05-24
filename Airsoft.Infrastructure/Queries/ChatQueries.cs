namespace Airsoft.Infrastructure.Queries
{
    public class ChatQueries
    {
        public static readonly string Save = 
        @"INSERT INTO Chat(ChatID,EsPrivado,nombreChat,Activo,FechaRegistro,UsuarioRegistroID)
          VALUES(@ChatID,@EsPrivado,@nombreChat,1,getdate(),@UsuarioRegistroID)";


        public static readonly string UpdateUnread = @"
                UPDATE Chat_Miembro SET MensajeNoleidos=0
                WHERE ChatID=@ChatID
                AND UsuarioID=@UsuarioID";

        public static readonly string AddUnread = @"
                UPDATE Chat_Miembro SET MensajeNoleidos+=1
                WHERE ChatID=@ChatID 
                AND UsuarioID<>@UsuarioID";
    }
}
