using MediatR;

namespace Swapy.BLL.Domain.Auth.Commands
{
    public class ForgotPasswordCommand : IRequest<Unit>
    {
        public string Email { get; set; }
    }
}
