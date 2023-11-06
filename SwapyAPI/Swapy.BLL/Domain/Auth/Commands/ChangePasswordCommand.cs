using MediatR;

namespace Swapy.BLL.Domain.Auth.Commands
{
    public class ChangePasswordCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
