namespace Swapy.Common.DTO.Chats.Responses
{
    public class MessageResponseDTO
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string ChatId { get; set; }
        public string SenderId { get; set; }
        public string SenderLogo { get; set; }
        public DateTime DateTime { get; set; }
    }
}
