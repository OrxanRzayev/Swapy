using MediatR;

namespace Swapy.BLL.Domain.Users.Commands
{
    public class RemoveUserCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
