using MediatR;
using Swapy.BLL.Domain.Items.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Items.QueryHandlers
{
    public class GetAllItemTypesQueryHandler : IRequestHandler<GetAllItemTypesQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllItemTypesQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllItemTypesQuery request, CancellationToken cancellationToken)
        {
            return await _subcategoryRepository.GetAllItemTypesAsync(request.ParentSubcategoryId);
        }
    }
}
