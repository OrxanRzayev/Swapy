using MediatR;
using Swapy.BLL.Domain.Clothes.Queries;
using Swapy.BLL.Domain.Electronics.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Electronics.QueryHandlers
{
    public class GetMemoryModelIdQueryHandler : IRequestHandler<GetMemoryModelIdQuery, string>
    {
        private readonly IMemoryModelRepository _memoryModelRepository;

        public GetMemoryModelIdQueryHandler(IMemoryModelRepository memoryModelRepository) => _memoryModelRepository = memoryModelRepository;

        public async Task<string> Handle(GetMemoryModelIdQuery request, CancellationToken cancellationToken)
        {
            return await _memoryModelRepository.GetByMemoryAndModelAsync(request.MemoryId, request.ModelId);
        }
    }
}
