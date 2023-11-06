using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllColorsQueryHandler : IRequestHandler<GetAllColorsQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly IColorRepository _colorRepository;

        public GetAllColorsQueryHandler(IColorRepository colorRepository) => _colorRepository = colorRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllColorsQuery request, CancellationToken cancellationToken)
        { 
            return await _colorRepository.GetAllSpecificationAsync();
        }
    }
}
