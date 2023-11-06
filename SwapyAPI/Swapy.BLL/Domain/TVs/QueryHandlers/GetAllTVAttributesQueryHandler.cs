using MediatR;
using Swapy.BLL.Domain.TVs.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.TVs.QueryHandlers
{
    public class GetAllTVAttributesQueryHandler : IRequestHandler<GetAllTVAttributesQuery, ProductsResponseDTO<ProductResponseDTO>>
    {
        private readonly ITVAttributeRepository _tvAttributeRepository;

        public GetAllTVAttributesQueryHandler(ITVAttributeRepository tvAttributeRepository) => _tvAttributeRepository = tvAttributeRepository;

        public async Task<ProductsResponseDTO<ProductResponseDTO>> Handle(GetAllTVAttributesQuery request, CancellationToken cancellationToken)
        {
            return await _tvAttributeRepository.GetAllFilteredAsync(request.Page,
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
                                                                    request.IsSmart,
                                                                    request.TVTypesId,
                                                                    request.TVBrandsId,
                                                                    request.ScreenResolutionsId,
                                                                    request.ScreenDiagonalsId,
                                                                    request.SortByPrice,
                                                                    request.ReverseSort);
        }
    }
}
