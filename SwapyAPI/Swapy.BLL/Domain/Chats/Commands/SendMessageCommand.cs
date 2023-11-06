using MediatR;
using Microsoft.AspNetCore.Http;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Chats.Commands
{
    public class SendMessageCommand : IRequest<Message>
    { 
        public string UserId { get; set; }
        public string Text { get; set; }
        public IFormFile? Image { get; set; }
        public string ChatId { get; set; }
    }
}