using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, Unit>
    {
        private readonly IProductImageRepository _productImageRepository;

        public UploadImageCommandHandler(IProductImageRepository productImageRepository) => _productImageRepository = productImageRepository;

        public async Task<Unit> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            foreach (var path in request.Paths)
            {
                var productImage = new ProductImage();
                productImage.ProductId = request.ProductId;
                productImage.Image = path;
                await _productImageRepository.CreateAsync(productImage);
            }
            return Unit.Value;
        }
    }
}
