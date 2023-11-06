using MediatR;
using Swapy.BLL.Domain.Electronics.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Electronics.QueryHandlers
{
    public class GetAllColorsQueryByModelHandler : IRequestHandler<GetAllColorsByModelQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly IColorRepository _colorRepository;

        public GetAllColorsQueryByModelHandler(IColorRepository colorRepository) => _colorRepository = colorRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllColorsByModelQuery request, CancellationToken cancellationToken)
        { 
            return await _colorRepository.GetByModelAsync(request.ModelId);
        }
    }
}
