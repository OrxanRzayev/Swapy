using MediatR;

namespace Swapy.BLL.Domain.Auth.Commands
{
    public class ResetPasswordCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
