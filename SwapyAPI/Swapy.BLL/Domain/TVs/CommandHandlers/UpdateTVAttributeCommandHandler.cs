using MediatR;
using Swapy.BLL.Domain.TVs.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.TVs.CommandHandlers
{
    public class UpdateTVAttributeCommandHandler : IRequestHandler<UpdateTVAttributeCommand, Unit>
    {
        private readonly IImageService _imageService;
        private readonly IProductRepository _productRepository;
        private readonly ITVAttributeRepository _tvAttributeRepository;

        public UpdateTVAttributeCommandHandler(IImageService imageService, IProductRepository productRepository, ITVAttributeRepository tvAttributeRepository)
        {
            _imageService = imageService;
            _productRepository = productRepository;
            _tvAttributeRepository = tvAttributeRepository;
        }

        public async Task<Unit> Handle(UpdateTVAttributeCommand request, CancellationToken cancellationToken)
        {
            var tvAttribute = await _tvAttributeRepository.GetByProductIdAsync(request.ProductId);
            var product = await _productRepository.GetByIdAsync(tvAttribute.ProductId);

            if (!request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to update this product");

            if (!string.IsNullOrEmpty(request.Title)) product.Title = request.Title;
            product.Description = request.Description;
            if (request.Price != null) product.Price = (decimal)request.Price;
            if (!string.IsNullOrEmpty(request.CurrencyId)) product.CurrencyId = request.CurrencyId;
            if (!string.IsNullOrEmpty(request.CategoryId)) product.CategoryId = request.CategoryId;
            if (!string.IsNullOrEmpty(request.SubcategoryId)) product.SubcategoryId = request.SubcategoryId;
            if (!string.IsNullOrEmpty(request.CityId)) product.CityId = request.CityId;
            await _productRepository.UpdateAsync(product);

            if (request.IsNew != null) tvAttribute.IsNew = (bool)request.IsNew;
            if (request.IsSmart != null) tvAttribute.IsSmart = (bool)request.IsSmart;
            if (!string.IsNullOrEmpty(request.TVTypeId)) tvAttribute.TVTypeId = request.TVTypeId;
            if (!string.IsNullOrEmpty(request.TVBrandId)) tvAttribute.TVBrandId = request.TVBrandId;
            if (!string.IsNullOrEmpty(request.ScreenResolutionId)) tvAttribute.ScreenResolutionId = request.ScreenResolutionId;
            if (!string.IsNullOrEmpty(request.ScreenDiagonalId)) tvAttribute.ScreenDiagonalId = request.ScreenDiagonalId;
            await _tvAttributeRepository.UpdateAsync(tvAttribute);

            if (request.OldPaths?.Count != null && request.OldPaths.Count > 0) await _imageService.RemoveProductImagesAsync(request.OldPaths, request.ProductId);

            if (request.NewFiles?.Count != null && request.NewFiles.Count > 0) await _imageService.UploadProductImagesAsync(request.NewFiles, request.ProductId);

            return Unit.Value;
        }
    }
}
