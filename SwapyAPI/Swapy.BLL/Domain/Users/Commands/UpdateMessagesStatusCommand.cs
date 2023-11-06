using MediatR;

namespace Swapy.BLL.Domain.Users.Commands
{
    public class UpdateMessagesStatusCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public bool Value { get; set; }
    }
}
