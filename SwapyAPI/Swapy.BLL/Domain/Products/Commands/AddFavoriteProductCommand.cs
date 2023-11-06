using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class AddFavoriteProductCommand : IRequest<FavoriteProduct>
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
    }
}
