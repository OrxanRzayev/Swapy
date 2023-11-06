using MediatR;
using Swapy.BLL.Domain.Items.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Items.QueryHandlers
{
    public class GetAllItemAttributesQueryHandler : IRequestHandler<GetAllItemAttributesQuery, ProductsResponseDTO<ProductResponseDTO>>
    {
        private readonly IItemAttributeRepository _itemAttributeRepository;

        public GetAllItemAttributesQueryHandler(IItemAttributeRepository itemAttributeRepository) => _itemAttributeRepository = itemAttributeRepository;

        public async Task<ProductsResponseDTO<ProductResponseDTO>> Handle(GetAllItemAttributesQuery request, CancellationToken cancellationToken)
        {
            return await _itemAttributeRepository.GetAllFilteredAsync(request.Page,
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
                                                                      request.ItemTypesId,
                                                                      request.SortByPrice,
                                                                      request.ReverseSort);
        }
    }
}
