namespace Airsoft.Infrastructure.Queries
{
    public class ChatQueries
    {
        public static readonly string Save = 
        @"INSERT INTO Chat(ChatID,EsPrivado,nombreChat,Activo,FechaRegistro,UsuarioRegistroID)
          VALUES(@ChatID,@EsPrivado,@nombreChat,1,getdate(),@UsuarioRegistroID)";
    }
}
