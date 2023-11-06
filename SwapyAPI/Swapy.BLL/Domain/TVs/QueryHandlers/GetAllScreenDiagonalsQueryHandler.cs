using MediatR;
using Swapy.BLL.Domain.TVs.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.TVs.QueryHandlers
{
    public class GetAllScreenDiagonalsQueryHandler : IRequestHandler<GetAllScreenDiagonalsQuery, IEnumerable<SpecificationResponseDTO<int>>>
    {
        private readonly IScreenDiagonalRepository _screenDiagonalRepository;

        public GetAllScreenDiagonalsQueryHandler(IScreenDiagonalRepository screenDiagonalRepository) => _screenDiagonalRepository = screenDiagonalRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<int>>> Handle(GetAllScreenDiagonalsQuery request, CancellationToken cancellationToken)
        {
            var result = (await _screenDiagonalRepository.GetAllAsync()).Select(x => new SpecificationResponseDTO<int>(x.Id, x.Diagonal));
            return result;
        }
    }
}
