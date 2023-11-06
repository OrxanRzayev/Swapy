using MediatR;
using Swapy.BLL.Domain.Shops.Queries;
using Swapy.Common.DTO.Shops.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Shops.QueryHandlers
{
    public class GetAllShopsQueryHandler : IRequestHandler<GetAllShopsQuery, ShopsResponseDTO>
    {
        private readonly IShopAttributeRepository _shopAttributeRepository;

        public GetAllShopsQueryHandler(IShopAttributeRepository shopAttributeRepository) => _shopAttributeRepository = shopAttributeRepository;

        public async Task<ShopsResponseDTO> Handle(GetAllShopsQuery request, CancellationToken cancellationToken)
        {
            return await _shopAttributeRepository.GetAllFilteredAsync(request.Page, request.PageSize, request.Title, request.SortByViews, request.ReverseSort);
        }
    }
}
