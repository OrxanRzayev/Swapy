using MediatR;

namespace Swapy.BLL.Domain.Auth.Commands
{
    public class ShopRegistrationCommand : IRequest<Unit>
    {
        public string ShopName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
