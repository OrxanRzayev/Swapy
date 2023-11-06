using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class IncrementProductViewsCommandHandler : IRequestHandler<IncrementProductViewsCommand, Unit>
    {
        private readonly IProductRepository _productRepository;

        public IncrementProductViewsCommandHandler(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<Unit> Handle(IncrementProductViewsCommand request, CancellationToken cancellationToken)
        {
            await _productRepository.IncrementViewsAsync(request.ProductId);
            return Unit.Value;
        }
    }
}
