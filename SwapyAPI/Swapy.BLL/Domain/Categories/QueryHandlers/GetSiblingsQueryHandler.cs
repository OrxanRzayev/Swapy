using MediatR;
using Swapy.BLL.Domain.Categories.Queries;
using Swapy.Common.DTO.Categories.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Categories.QueryHandlers
{
    public class GetSiblingsQueryHandler : IRequestHandler<GetSiblingsQuery, IEnumerable<CategoryTreeResponseDTO>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetSiblingsQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<CategoryTreeResponseDTO>> Handle(GetSiblingsQuery request, CancellationToken cancellationToken)
        {
            return await _subcategoryRepository.GetSiblings(request.SubcategoryId);
        }
    }
}
