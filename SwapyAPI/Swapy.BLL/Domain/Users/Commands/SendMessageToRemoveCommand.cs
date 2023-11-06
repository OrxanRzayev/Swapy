using MediatR;

namespace Swapy.BLL.Domain.Users.Commands
{
    public class SendMessageToRemoveCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
    }
}
