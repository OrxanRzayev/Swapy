using MediatR;
using Swapy.BLL.Domain.Electronics.Queries;
using Swapy.Common.DTO.Electronics.Responses;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Electronics.QueryHandlers
{
    public class GetByIdElectronicAttributeQueryHandler : IRequestHandler<GetByIdElectronicAttributeQuery, ElectronicAttributeResponseDTO>
    {
        private readonly IElectronicAttributeRepository _electronicAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public GetByIdElectronicAttributeQueryHandler(IElectronicAttributeRepository electronicAttributeRepository, ISubcategoryRepository subcategoryRepository, IFavoriteProductRepository favoriteProductRepository)
        {
            _electronicAttributeRepository = electronicAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
            _favoriteProductRepository = favoriteProductRepository;
        }

        public async Task<ElectronicAttributeResponseDTO> Handle(GetByIdElectronicAttributeQuery request, CancellationToken cancellationToken)
        {
            var electronicAttribute = await _electronicAttributeRepository.GetDetailByIdAsync(request.ProductId);
            List<SpecificationResponseDTO<string>> categories = (await _subcategoryRepository.GetSequenceOfSubcategories(electronicAttribute.Product.SubcategoryId)).ToList();
            categories.Insert(0, new SpecificationResponseDTO<string>(electronicAttribute.Product.CategoryId, electronicAttribute.Product.Category.Name));

            ElectronicAttributeResponseDTO result = new ElectronicAttributeResponseDTO()
            {
                Id = electronicAttribute.Id,
                City = electronicAttribute.Product.City.Name,
                CityId = electronicAttribute.Product.CityId,
                Currency = electronicAttribute.Product.Currency.Name,
                CurrencyId = electronicAttribute.Product.CurrencyId,
                CurrencySymbol = electronicAttribute.Product.Currency.Symbol,
                UserId = electronicAttribute.Product.UserId,
                FirstName = electronicAttribute.Product.User.FirstName,
                LastName = electronicAttribute.Product.User.LastName,
                PhoneNumber = electronicAttribute.Product.User.PhoneNumber,
                ShopId = electronicAttribute.Product.User.ShopAttributeId ?? string.Empty,
                Shop = electronicAttribute.Product.User.ShopAttribute?.ShopName ?? string.Empty,
                UserType = electronicAttribute.Product.User.Type,
                ProductId = electronicAttribute.ProductId,
                Title = electronicAttribute.Product.Title,
                Views = electronicAttribute.Product.Views,
                Price = electronicAttribute.Product.Price,
                Description = electronicAttribute.Product?.Description,
                DateTime = electronicAttribute.Product.DateTime,
                Categories = categories,
                IsDisable = electronicAttribute.Product.IsDisable,
                Images = electronicAttribute.Product.Images.Select(i => i.Image).ToList(),
                IsNew = electronicAttribute.IsNew,
                IsFavorite = request.UserId == null ? false : await _favoriteProductRepository.CheckProductOnFavorite(electronicAttribute.ProductId, request.UserId),
                ColorId = electronicAttribute.ModelColor.ColorId,
                Color = electronicAttribute.ModelColor.Color.Name,
                MemoryId = electronicAttribute.MemoryModel.MemoryId,
                Memory = electronicAttribute.MemoryModel.Memory.Name,
                ModelId = electronicAttribute.ModelColor.ModelId,
                Model = electronicAttribute.ModelColor.Model.Name,
                ElectronicBrandId = electronicAttribute.ModelColor.Model.ElectronicBrandType.ElectronicBrandId,
                ElectronicBrand = electronicAttribute.ModelColor.Model.ElectronicBrandType.ElectronicBrand.Name
            };
            return result;
        }
    }
}
