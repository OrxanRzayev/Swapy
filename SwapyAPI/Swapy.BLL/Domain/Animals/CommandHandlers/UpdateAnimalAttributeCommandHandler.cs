using MediatR;
using Swapy.BLL.Domain.Animals.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Animals.CommandHandlers
{
    public class UpdateAnimalAttributeCommandHandler : IRequestHandler<UpdateAnimalAttributeCommand, Unit>
    {
        private readonly IImageService _imageService;
        private readonly IProductRepository _productRepository;
        private readonly IAnimalAttributeRepository _animalAttributeRepository;

        public UpdateAnimalAttributeCommandHandler(IImageService imageService, IProductRepository productRepository, IAnimalAttributeRepository animalAttributeRepository)
        {
            _imageService = imageService;
            _productRepository = productRepository;
            _animalAttributeRepository = animalAttributeRepository;
        }

        public async Task<Unit> Handle(UpdateAnimalAttributeCommand request, CancellationToken cancellationToken)
        {
            var animalAttribute = await _animalAttributeRepository.GetByProductIdAsync(request.ProductId);
            var product = await _productRepository.GetByIdAsync(animalAttribute.ProductId);

            if (!request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to update this product");

            if (!string.IsNullOrEmpty(request.Title)) product.Title = request.Title;
            product.Description = request.Description;
            if (request.Price != null) product.Price = (decimal)request.Price;
            if (!string.IsNullOrEmpty(request.CurrencyId)) product.CurrencyId = request.CurrencyId;
            if (!string.IsNullOrEmpty(request.CategoryId)) product.CategoryId = request.CategoryId;
            if (!string.IsNullOrEmpty(request.SubcategoryId)) product.SubcategoryId = request.SubcategoryId;
            if (!string.IsNullOrEmpty(request.CityId)) product.CityId = request.CityId;
            await _productRepository.UpdateAsync(product);

            if (!string.IsNullOrEmpty(request.AnimalBreedId)) animalAttribute.AnimalBreedId = request.AnimalBreedId;
            await _animalAttributeRepository.UpdateAsync(animalAttribute);

            if (request.OldPaths?.Count > 0) await _imageService.RemoveProductImagesAsync(request.OldPaths, request.ProductId);

            if (request.NewFiles?.Count > 0) await _imageService.UploadProductImagesAsync(request.NewFiles, request.ProductId);

            return Unit.Value;
        }
    }
}
