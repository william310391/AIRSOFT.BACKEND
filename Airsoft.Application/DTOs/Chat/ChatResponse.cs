namespace Airsoft.Application.DTOs.Chat
{
    public class ChatResponse
    {
        public Guid ChatID { get; set; }
        public bool EsPrivado { get; set; }
        public required string NombreChat { get; set; }
        public int MensajeNoleidos { get; set; }
        public int UsuarioID { get; set; }
    }
}
