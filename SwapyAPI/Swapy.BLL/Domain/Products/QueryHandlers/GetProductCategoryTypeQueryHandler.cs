
using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetProductCategoryTypeQueryHandler : IRequestHandler<GetProductCategoryTypeQuery, SpecificationResponseDTO<CategoryType>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductCategoryTypeQueryHandler(IProductRepository productRepository, IFavoriteProductRepository favoriteProductRepository) => _productRepository = productRepository;

        public async Task<SpecificationResponseDTO<CategoryType>> Handle(GetProductCategoryTypeQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetProductCategoryTypeAsync(request.ProductId);
        }
    }
}
