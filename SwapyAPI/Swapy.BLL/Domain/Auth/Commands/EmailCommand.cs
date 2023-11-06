using MediatR;

namespace Swapy.BLL.Domain.Auth.Commands
{
    public class EmailCommand : IRequest<bool>
    {
        public string Email { get; set; }
    }
}
