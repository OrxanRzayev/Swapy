using MediatR;
using Swapy.BLL.Domain.Categories.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Categories.QueryHandlers
{
    public class GetSubcategoryPathQueryHandler : IRequestHandler<GetSubcategoryPathQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly ICategoryRepository _categoryRepository;

        public GetSubcategoryPathQueryHandler(ISubcategoryRepository subcategoryRepository, ICategoryRepository categoryRepository)
        {
            _subcategoryRepository = subcategoryRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetSubcategoryPathQuery request, CancellationToken cancellationToken)
        {
            List<SpecificationResponseDTO<string>> categories = (await _subcategoryRepository.GetSequenceOfSubcategories(request.SubcategoryId)).ToList();
            categories.Insert(0, await _categoryRepository.GetBySubcategoryIdAsync(categories[0]?.Id));
            return categories;
        }
    }
}
