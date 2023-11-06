using MediatR;
using Swapy.BLL.Domain.Items.Queries;
using Swapy.Common.DTO.Items.Responses;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Items.QueryHandlers
{
    public class GetByIdItemAttributeQueryHandler : IRequestHandler<GetByIdItemAttributeQuery, ItemAttributeResponseDTO>
    {
        private readonly IItemAttributeRepository _itemAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public GetByIdItemAttributeQueryHandler(IItemAttributeRepository itemAttributeRepository, ISubcategoryRepository subcategoryRepository, IFavoriteProductRepository favoriteProductRepository)
        {
            _itemAttributeRepository = itemAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
            _favoriteProductRepository = favoriteProductRepository;
        }

        public async Task<ItemAttributeResponseDTO> Handle(GetByIdItemAttributeQuery request, CancellationToken cancellationToken)
        {
            var itemAttribute = await _itemAttributeRepository.GetDetailByIdAsync(request.ProductId);
            List<SpecificationResponseDTO<string>> categories = (await _subcategoryRepository.GetSequenceOfSubcategories(itemAttribute.Product.SubcategoryId)).ToList();
            categories.Insert(0, new SpecificationResponseDTO<string>(itemAttribute.Product.CategoryId, itemAttribute.Product.Category.Name));

            ItemAttributeResponseDTO result = new ItemAttributeResponseDTO()
            {
                Id = itemAttribute.Id,
                City = itemAttribute.Product.City.Name,
                CityId = itemAttribute.Product.CityId,
                Currency = itemAttribute.Product.Currency.Name,
                CurrencyId = itemAttribute.Product.CurrencyId,
                CurrencySymbol = itemAttribute.Product.Currency.Symbol,
                UserId = itemAttribute.Product.UserId,
                FirstName = itemAttribute.Product.User.FirstName,
                LastName = itemAttribute.Product.User.LastName,
                PhoneNumber = itemAttribute.Product.User.PhoneNumber,
                ShopId = itemAttribute.Product.User.ShopAttributeId ?? string.Empty,
                Shop = itemAttribute.Product.User.ShopAttribute?.ShopName ?? string.Empty,
                UserType = itemAttribute.Product.User.Type,
                ProductId = itemAttribute.ProductId,
                IsDisable = itemAttribute.Product.IsDisable,
                Title = itemAttribute.Product.Title,
                Views = itemAttribute.Product.Views,
                Price = itemAttribute.Product.Price,
                Description = itemAttribute.Product?.Description,
                DateTime = itemAttribute.Product.DateTime,
                Categories = categories,
                Images = itemAttribute.Product.Images.Select(i => i.Image).ToList(),
                IsNew = itemAttribute.IsNew,
                IsFavorite = request.UserId == null ? false : await _favoriteProductRepository.CheckProductOnFavorite(itemAttribute.ProductId, request.UserId)
            };
            return result;
        }
    }
}
