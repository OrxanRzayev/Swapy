using MediatR;
using Swapy.BLL.Domain.Clothes.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;

namespace Swapy.BLL.Domain.Clothes.CommandHandlers
{
    public class UpdateClothesAttributeCommandHandler : IRequestHandler<UpdateClothesAttributeCommand, Unit>
    {
        private readonly IImageService _imageService;
        private readonly IProductRepository _productRepository;
        private readonly IClothesAttributeRepository _clothesAttributeRepository;

        public UpdateClothesAttributeCommandHandler(IImageService imageService, IProductRepository productRepository, IClothesAttributeRepository clothesAttributeRepository)
        {
            _imageService = imageService;
            _productRepository = productRepository;
            _clothesAttributeRepository = clothesAttributeRepository;
        }

        public async Task<Unit> Handle(UpdateClothesAttributeCommand request, CancellationToken cancellationToken)
        {

            var clothesAttribute = await _clothesAttributeRepository.GetByProductIdAsync(request.ProductId);
            var product = await _productRepository.GetByIdAsync(clothesAttribute.ProductId);

            if (!request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to update this product");

            if (!string.IsNullOrEmpty(request.Title)) product.Title = request.Title;
            product.Description = request.Description;
            if (request.Price != null) product.Price = (decimal)request.Price;
            if (!string.IsNullOrEmpty(request.CurrencyId)) product.CurrencyId = request.CurrencyId;
            if (!string.IsNullOrEmpty(request.CategoryId)) product.CategoryId = request.CategoryId;
            if (!string.IsNullOrEmpty(request.SubcategoryId)) product.SubcategoryId = request.SubcategoryId;
            if (!string.IsNullOrEmpty(request.CityId)) product.CityId = request.CityId;
            await _productRepository.UpdateAsync(product);

            if (request.IsNew != null) clothesAttribute.IsNew = (bool)request.IsNew;
            if (!string.IsNullOrEmpty(request.ClothesSeasonId)) clothesAttribute.ClothesSeasonId = request.ClothesSeasonId;
            if (!string.IsNullOrEmpty(request.ClothesSizeId)) clothesAttribute.ClothesSizeId = request.ClothesSizeId;
            if (!string.IsNullOrEmpty(request.ClothesBrandViewId)) clothesAttribute.ClothesBrandViewId = request.ClothesBrandViewId;
            await _clothesAttributeRepository.UpdateAsync(clothesAttribute);

            if (request.OldPaths?.Count > 0) await _imageService.RemoveProductImagesAsync(request.OldPaths, request.ProductId);

            if (request.NewFiles?.Count > 0) await _imageService.UploadProductImagesAsync(request.NewFiles, request.ProductId);

            return Unit.Value;
        }
    }
}