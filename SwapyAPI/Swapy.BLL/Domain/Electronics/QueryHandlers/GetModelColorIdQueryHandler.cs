using MediatR;
using Swapy.BLL.Domain.Electronics.Queries;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Electronics.QueryHandlers
{
    public class GetModelColorIdQueryHandler : IRequestHandler<GetModelColorIdQuery, string>
    {
        private readonly IModelColorRepository _modelColorRepository;

        public GetModelColorIdQueryHandler(IModelColorRepository modelColorRepository) => _modelColorRepository = modelColorRepository;

        public async Task<string> Handle(GetModelColorIdQuery request, CancellationToken cancellationToken)
        {
            return await _modelColorRepository.GetByModelAndColorAsync(request.ModelId, request.ColorId);
        }
    }
}
