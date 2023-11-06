using MediatR;
using Swapy.BLL.Domain.Categories.Queries;
using Swapy.Common.DTO.Categories.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Categories.QueryHandlers
{
    public class GetAllSubcategoriesByCategoryQueryHandler : IRequestHandler<GetAllSubcategoriesByCategoryQuery, IEnumerable<CategoryTreeResponseDTO>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllSubcategoriesByCategoryQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<CategoryTreeResponseDTO>> Handle(GetAllSubcategoriesByCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _subcategoryRepository.GetByCategoryAsync(request.CategoryId);
        }
    }
}
