using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class IncrementProductViewsCommand : IRequest<Unit>
    {
        public string ProductId { get; set; }
    }
}
