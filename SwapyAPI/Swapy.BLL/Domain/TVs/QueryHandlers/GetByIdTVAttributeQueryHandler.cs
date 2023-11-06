using MediatR;
using Swapy.BLL.Domain.TVs.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.DTO.TVs.Responses;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.TVs.QueryHandlers
{
    public class GetByIdTVAttributeQueryHandler : IRequestHandler<GetByIdTVAttributeQuery, TVAttributeResponseDTO>
    {
        private readonly ITVAttributeRepository _tvAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public GetByIdTVAttributeQueryHandler(ITVAttributeRepository tvAttributeRepository, ISubcategoryRepository subcategoryRepository, IFavoriteProductRepository favoriteProductRepository)
        {
            _tvAttributeRepository = tvAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
            _favoriteProductRepository = favoriteProductRepository;
        }

        public async Task<TVAttributeResponseDTO> Handle(GetByIdTVAttributeQuery request, CancellationToken cancellationToken)
        {
            var tvAttribute = await _tvAttributeRepository.GetDetailByIdAsync(request.ProductId);
            List<SpecificationResponseDTO<string>> categories = (await _subcategoryRepository.GetSequenceOfSubcategories(tvAttribute.Product.SubcategoryId)).ToList();
            categories.Insert(0, new SpecificationResponseDTO<string>(tvAttribute.Product.CategoryId, tvAttribute.Product.Category.Name));

            TVAttributeResponseDTO result = new TVAttributeResponseDTO()
            {
                Id = tvAttribute.Id,
                City = tvAttribute.Product.City.Name,
                CityId = tvAttribute.Product.CityId,
                Currency = tvAttribute.Product.Currency.Name,
                CurrencyId = tvAttribute.Product.CurrencyId,
                CurrencySymbol = tvAttribute.Product.Currency.Symbol,
                UserId = tvAttribute.Product.UserId,
                FirstName = tvAttribute.Product.User.FirstName,
                LastName = tvAttribute.Product.User.LastName,
                PhoneNumber = tvAttribute.Product.User.PhoneNumber,
                ShopId = tvAttribute.Product.User.ShopAttributeId ?? string.Empty,
                Shop = tvAttribute.Product.User.ShopAttribute?.ShopName ?? string.Empty,
                UserType = tvAttribute.Product.User.Type,
                ProductId = tvAttribute.ProductId,
                Title = tvAttribute.Product.Title,
                Views = tvAttribute.Product.Views,
                Price = tvAttribute.Product.Price,
                Description = tvAttribute.Product?.Description,
                DateTime = tvAttribute.Product.DateTime,
                IsDisable = tvAttribute.Product.IsDisable,
                Categories = categories,
                Images = tvAttribute.Product.Images.Select(i => i.Image).ToList(),
                IsNew = tvAttribute.IsNew,
                IsFavorite = request.UserId == null ? false : await _favoriteProductRepository.CheckProductOnFavorite(tvAttribute.ProductId, request.UserId),
                IsSmart = tvAttribute.IsSmart,
                TVBrandId = tvAttribute.TVBrandId,
                TVBrand = tvAttribute.TVBrand.Name,
                TVTypeId = tvAttribute.TVTypeId,
                TVType = tvAttribute.TVType.Name,
                ScreenDiagonalId = tvAttribute.ScreenDiagonalId,
                ScreenDiagonal = tvAttribute.ScreenDiagonal.Diagonal,
                ScreenResolutionId = tvAttribute.ScreenResolutionId,
                ScreenResolution = tvAttribute.ScreenResolution.Name
            };
            return result;
        }
    }
}
