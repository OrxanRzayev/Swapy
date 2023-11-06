using MediatR;

namespace Swapy.BLL.Domain.Auth.Commands
{
    public class PhoneNumberCommand : IRequest<bool>
    {
        public string PhoneNumber { get; set; }
    }
}
