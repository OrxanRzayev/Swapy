using MediatR;
using Swapy.BLL.Domain.Items.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;

namespace Swapy.BLL.Domain.Items.CommandHandlers
{
    public class UpdateItemAttributeCommandHandler : IRequestHandler<UpdateItemAttributeCommand, Unit>
    {
        private readonly IImageService _imageService;
        private readonly IProductRepository _productRepository;
        private readonly IItemAttributeRepository _itemAttributeRepository;

        public UpdateItemAttributeCommandHandler(IImageService imageService, IProductRepository productRepository, IItemAttributeRepository itemAttributeRepository)
        {
            _imageService = imageService;
            _productRepository = productRepository;
            _itemAttributeRepository = itemAttributeRepository;
        }

        public async Task<Unit> Handle(UpdateItemAttributeCommand request, CancellationToken cancellationToken)
        {

            var itemAttribute = await _itemAttributeRepository.GetByProductIdAsync(request.ProductId);
            var product = await _productRepository.GetByIdAsync(itemAttribute.ProductId);

            if (!request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to update this product");

            if (!string.IsNullOrEmpty(request.Title)) product.Title = request.Title;
            product.Description = request.Description;
            if (request.Price != null) product.Price = (decimal)request.Price;
            if (!string.IsNullOrEmpty(request.CurrencyId)) product.CurrencyId = request.CurrencyId;
            if (!string.IsNullOrEmpty(request.CategoryId)) product.CategoryId = request.CategoryId;
            if (!string.IsNullOrEmpty(request.SubcategoryId)) product.SubcategoryId = request.SubcategoryId;
            if (!string.IsNullOrEmpty(request.CityId)) product.CityId = request.CityId;
            await _productRepository.UpdateAsync(product);

            if (request.IsNew != null) itemAttribute.IsNew = (bool)request.IsNew;
            if (!string.IsNullOrEmpty(request.ItemTypeId)) itemAttribute.ItemTypeId = request.ItemTypeId;
            await _itemAttributeRepository.UpdateAsync(itemAttribute);

            if (request.OldPaths?.Count > 0) await _imageService.RemoveProductImagesAsync(request.OldPaths, request.ProductId);

            if (request.NewFiles?.Count > 0) await _imageService.UploadProductImagesAsync(request.NewFiles, request.ProductId);

            return Unit.Value;
        }
    }
}
