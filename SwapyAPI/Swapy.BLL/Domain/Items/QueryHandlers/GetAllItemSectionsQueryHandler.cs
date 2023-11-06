using MediatR;
using Swapy.BLL.Domain.Items.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Items.QueryHandlers
{
    public class GetAllItemSectionsQueryHandler : IRequestHandler<GetAllItemSectionsQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllItemSectionsQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllItemSectionsQuery request, CancellationToken cancellationToken)
        {
            return await _subcategoryRepository.GetAllItemSectionsAsync();
        }
    }
}
