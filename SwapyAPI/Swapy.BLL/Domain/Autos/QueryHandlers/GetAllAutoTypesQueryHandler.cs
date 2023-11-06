using MediatR;
using Swapy.BLL.Domain.Autos.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Autos.QueryHandlers
{
    public class GetAllAutoTypesQueryHandler : IRequestHandler<GetAllAutoTypesQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllAutoTypesQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllAutoTypesQuery request, CancellationToken cancellationToken)
        {
            return await _subcategoryRepository.GetAllAutoTypesAsync();
        }
    }
}
