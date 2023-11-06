using MediatR;
using Swapy.BLL.Domain.Clothes.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Clothes.QueryHandlers
{
    public class GetAllClothesAttributesQueryHandler : IRequestHandler<GetAllClothesAttributesQuery, ProductsResponseDTO<ProductResponseDTO>>
    {
        private readonly IClothesAttributeRepository _clothesAttributeRepository;

        public GetAllClothesAttributesQueryHandler(IClothesAttributeRepository clothesAttributeRepository, IFavoriteProductRepository favoriteProductRepository) => _clothesAttributeRepository = clothesAttributeRepository;

        public async Task<ProductsResponseDTO<ProductResponseDTO>> Handle(GetAllClothesAttributesQuery request, CancellationToken cancellationToken)
        {
            return await _clothesAttributeRepository.GetAllFilteredAsync(request.Page,
                                                                         request.PageSize,
                                                                         request.UserId,
                                                                         request.Title,
                                                                         request.CurrencyId,
                                                                         request.PriceMin,
                                                                         request.PriceMax,
                                                                         request.CategoryId,
                                                                         request.SubcategoryId,
                                                                         request.CityId,
                                                                         request.OtherUserId,
                                                                         request.IsNew,
                                                                         request.ClothesSeasonsId,
                                                                         request.ClothesSizesId,
                                                                         request.ClothesBrandsId,
                                                                         request.ClothesViewsId,
                                                                         request.ClothesTypesId,
                                                                         request.ClothesGendersId,
                                                                         request.IsChild,
                                                                         request.SortByPrice,
                                                                         request.ReverseSort);
        }
    }
}
