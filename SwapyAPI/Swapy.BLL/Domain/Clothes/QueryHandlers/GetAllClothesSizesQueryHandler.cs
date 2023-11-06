using MediatR;
using Swapy.BLL.Domain.Clothes.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Clothes.QueryHandlers
{
    public class GetAllClothesSizesQueryHandler : IRequestHandler<GetAllClothesSizesQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly IClothesSizeRepository _clothesSizeRepository;

        public GetAllClothesSizesQueryHandler(IClothesSizeRepository clothesSizeRepository) => _clothesSizeRepository = clothesSizeRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllClothesSizesQuery request, CancellationToken cancellationToken)
        {
            var result = (await _clothesSizeRepository.GetByChildAndShoeAsync(request.IsChild, request.IsShoe)).Select(x => new SpecificationResponseDTO<string>(x.Id, x.Size));
            return result;
        }
    }
}
