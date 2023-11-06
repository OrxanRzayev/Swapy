using MediatR;
using Swapy.BLL.Domain.Electronics.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Electronics.QueryHandlers
{
    public class GetAllMemoriesQueryHandler : IRequestHandler<GetAllMemoriesQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly IMemoryRepository _memoryRepository;

        public GetAllMemoriesQueryHandler(IMemoryRepository memoryRepository) => _memoryRepository = memoryRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllMemoriesQuery request, CancellationToken cancellationToken)
        {
            var result = (await _memoryRepository.GetByModelAsync(request.ModelId)).Select(x => new SpecificationResponseDTO<string>(x.Id, x.Name));
            return result;
        }
    }
}
