using MediatR;

namespace Swapy.BLL.Domain.Auth.Commands
{
    public class LogoutCommand : IRequest<Unit>
    {
        public string RefreshToken { get; set; }
    }
}
