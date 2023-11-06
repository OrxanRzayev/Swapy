using MediatR;
using Swapy.BLL.Domain.Electronics.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Electronics.QueryHandlers
{
    public class GetAllElectronicAttributesQueryHandler : IRequestHandler<GetAllElectronicAttributesQuery, ProductsResponseDTO<ProductResponseDTO>>
    {
        private readonly IElectronicAttributeRepository _electronicAttributeRepository;

        public GetAllElectronicAttributesQueryHandler(IElectronicAttributeRepository electronicAttributeRepository, IFavoriteProductRepository favoriteProductRepository) => _electronicAttributeRepository = electronicAttributeRepository;

        public async Task<ProductsResponseDTO<ProductResponseDTO>> Handle(GetAllElectronicAttributesQuery request, CancellationToken cancellationToken)
        {
            return await _electronicAttributeRepository.GetAllFilteredAsync(request.Page,
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
                                                                        request.MemoriesId,
                                                                        request.ColorsId,
                                                                        request.ModelsId,
                                                                        request.BrandsId,
                                                                        request.TypesId,
                                                                        request.SortByPrice,
                                                                        request.ReverseSort);
        }
    }
}
