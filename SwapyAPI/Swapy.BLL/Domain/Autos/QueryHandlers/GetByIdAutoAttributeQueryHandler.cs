using MediatR;
using Swapy.BLL.Domain.Autos.Queries;
using Swapy.Common.DTO.Autos.Responses;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Autos.QueryHandlers
{
    public class GetByIdAutoAttributeQueryHandler : IRequestHandler<GetByIdAutoAttributeQuery, AutoAttributeResponseDTO>
    {
        private readonly IAutoAttributeRepository _autoAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public GetByIdAutoAttributeQueryHandler(IAutoAttributeRepository autoAttributeRepository, ISubcategoryRepository subcategoryRepository, IFavoriteProductRepository favoriteProductRepository)
        {
            _autoAttributeRepository = autoAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
            _favoriteProductRepository = favoriteProductRepository;
        }

        public async Task<AutoAttributeResponseDTO> Handle(GetByIdAutoAttributeQuery request, CancellationToken cancellationToken)
        {
            var autoAttribute = await _autoAttributeRepository.GetDetailByIdAsync(request.ProductId);
            List<SpecificationResponseDTO<string>> categories = (await _subcategoryRepository.GetSequenceOfSubcategories(autoAttribute.Product.SubcategoryId)).ToList();
            categories.Insert(0, new SpecificationResponseDTO<string>(autoAttribute.Product.CategoryId, autoAttribute.Product.Category.Name));

            AutoAttributeResponseDTO result = new AutoAttributeResponseDTO()
            {
                Id = autoAttribute.Id,
                City = autoAttribute.Product.City.Name,
                CityId = autoAttribute.Product.CityId,
                Currency = autoAttribute.Product.Currency.Name,
                CurrencyId = autoAttribute.Product.CurrencyId,
                CurrencySymbol = autoAttribute.Product.Currency.Symbol,
                UserId = autoAttribute.Product.UserId,
                FirstName = autoAttribute.Product.User.FirstName,
                LastName = autoAttribute.Product.User.LastName,
                PhoneNumber = autoAttribute.Product.User.PhoneNumber,
                ShopId = autoAttribute.Product.User.ShopAttributeId ?? string.Empty,
                Shop = autoAttribute.Product.User.ShopAttribute?.ShopName ?? string.Empty,
                UserType = autoAttribute.Product.User.Type,
                ProductId = autoAttribute.ProductId,
                Title = autoAttribute.Product.Title,
                Views = autoAttribute.Product.Views,
                Price = autoAttribute.Product.Price,
                Description = autoAttribute.Product?.Description,
                DateTime = autoAttribute.Product.DateTime,
                Categories = categories,
                Images = autoAttribute.Product.Images.Select(i => i.Image).ToList(),
                IsNew = autoAttribute.IsNew,
                IsFavorite = request.UserId == null ? false : await _favoriteProductRepository.CheckProductOnFavorite(autoAttribute.ProductId, request.UserId),
                IsDisable = autoAttribute.Product.IsDisable,
                Miliage = autoAttribute.Miliage,
                EngineCapacity = autoAttribute.EngineCapacity,
                ReleaseYear = autoAttribute.ReleaseYear,
                FuelTypeId = autoAttribute.FuelTypeId,
                FuelType = autoAttribute.FuelType.Name,
                ColorId = autoAttribute.AutoColorId,
                Color = autoAttribute.AutoColor.Name,
                TransmissionTypeId = autoAttribute.TransmissionTypeId,
                TransmissionType = autoAttribute.TransmissionType.Name,
                AutoBrandId = autoAttribute.AutoModel.AutoBrandId,
                AutoBrand = autoAttribute.AutoModel.AutoBrand.Name,
                AutoModelId = autoAttribute.AutoModelId,
                AutoModel = autoAttribute.AutoModel.Name
            };
            return result;
        }
    }
}
