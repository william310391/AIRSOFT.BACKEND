namespace Airsoft.Infrastructure.Queries
{
    public class ChatMiembroQueries
    {
        public static readonly string Save =
              @"INSERT INTO Chat_Miembro(ChatID,UsuarioID,EsAdmin,Activo,UsuarioRegistroID)
              VALUES(@ChatID,@UsuarioID,@EsAdmin,@Activo,@UsuarioRegistroID)";
    }
}
