using MediatR;

namespace Swapy.BLL.Domain.Auth.Commands
{
    public class ShopNameCommand : IRequest<bool>
    {
        public string ShopName { get; set; }
    }
}
