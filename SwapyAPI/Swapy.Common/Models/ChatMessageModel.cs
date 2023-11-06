namespace Swapy.Common.Models
{
    public class ChatMessageModel
    {
        public string ChatId { get; set; }
        public string RecepientId { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public DateTime DateTime { get; set; }

        public ChatMessageModel(string chatId, string recepientId, string senderId, string senderName, string message, string image, DateTime dateTime)
        {
            ChatId = chatId;
            RecepientId = recepientId;
            SenderId = senderId;
            SenderName = senderName;
            Message = message;
            Image = image;
            DateTime = dateTime;
        }
    }
}