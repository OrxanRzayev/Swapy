using MediatR;

namespace Swapy.BLL.Domain.Auth.Commands
{
    public class ConfirmEmailCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
