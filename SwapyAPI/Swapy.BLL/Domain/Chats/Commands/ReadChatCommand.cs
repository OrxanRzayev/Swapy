using MediatR;

namespace Swapy.BLL.Domain.Chats.Commands
{
    public class ReadChatCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string ChatId { get; set; }
    }
}
