using MediatR;
using Swapy.BLL.Domain.RealEstates.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.RealEstates.QueryHandlers
{
    public class GetAllRealEstateTypesQueryHandler : IRequestHandler<GetAllRealEstateTypesQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllRealEstateTypesQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllRealEstateTypesQuery request, CancellationToken cancellationToken)
        {
            return await _subcategoryRepository.GetAllRealEstateTypesAsync();
        }
    }
}
