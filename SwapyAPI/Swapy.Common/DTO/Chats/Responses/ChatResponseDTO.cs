namespace Swapy.Common.DTO.Chats.Responses
{
    public class ChatResponseDTO
    {
        public string ChatId { get; set; }
        public string Title { get; set; }
        public string Logo { get; set; }
        public string Image { get; set; }
        public string LastMessage { get; set; }
        public bool IsReaded { get; set; }
        public DateTime? LastMessageDateTime { get; set; }
    }
}
