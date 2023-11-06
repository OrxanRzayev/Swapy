using MediatR;
using Swapy.BLL.Domain.Autos.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Autos.CommandHandlers
{
    public class UpdateAutoAttributeCommandHandler : IRequestHandler<UpdateAutoAttributeCommand, Unit>
    {
        private readonly IImageService _imageService;
        private readonly IProductRepository _productRepository;
        private readonly IAutoAttributeRepository _autoAttributeRepository;

        public UpdateAutoAttributeCommandHandler(IImageService imageService, IProductRepository productRepository, IAutoAttributeRepository autoAttributeRepository)
        {
            _imageService = imageService;
            _productRepository = productRepository;
            _autoAttributeRepository = autoAttributeRepository;
        }

        public async Task<Unit> Handle(UpdateAutoAttributeCommand request, CancellationToken cancellationToken)
        {
            var autoAttribute = await _autoAttributeRepository.GetByProductIdAsync(request.ProductId);
            var product = await _productRepository.GetByIdAsync(autoAttribute.ProductId);

            if (!request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to uninstall this product");

            if(!string.IsNullOrEmpty(request.Title)) product.Title = request.Title;
            product.Description = request.Description;
            if (request.Price != null) product.Price = (decimal)request.Price;
            if(!string.IsNullOrEmpty(request.CurrencyId)) product.CurrencyId = request.CurrencyId;
            if(!string.IsNullOrEmpty(request.CategoryId)) product.CategoryId = request.CategoryId;
            if(!string.IsNullOrEmpty(request.SubcategoryId)) product.SubcategoryId = request.SubcategoryId;
            if (!string.IsNullOrEmpty(request.CityId)) product.CityId = request.CityId;
            await _productRepository.UpdateAsync(product);

            if (request.Miliage != null) autoAttribute.Miliage = (int)request.Miliage;
            if (request.EngineCapacity != null) autoAttribute.EngineCapacity = (int)request.EngineCapacity;
            if (request.ReleaseYear != null) autoAttribute.ReleaseYear = (DateTime)request.ReleaseYear;
            if (request.IsNew != null) autoAttribute.IsNew = (bool)request.IsNew;
            if (!string.IsNullOrEmpty(request.FuelTypeId)) autoAttribute.FuelTypeId = request.FuelTypeId;
            if (!string.IsNullOrEmpty(request.AutoColorId)) autoAttribute.AutoColorId = request.AutoColorId;
            if (!string.IsNullOrEmpty(request.TransmissionTypeId)) autoAttribute.TransmissionTypeId = request.TransmissionTypeId;
            if (!string.IsNullOrEmpty(request.AutoModelId)) autoAttribute.AutoModelId = request.AutoModelId;
            await _autoAttributeRepository.UpdateAsync(autoAttribute);

            if (request.OldPaths?.Count > 0) await _imageService.RemoveProductImagesAsync(request.OldPaths, request.ProductId);

            if (request.NewFiles?.Count > 0) await _imageService.UploadProductImagesAsync(request.NewFiles, request.ProductId);

            return Unit.Value;
        }
    }
}
