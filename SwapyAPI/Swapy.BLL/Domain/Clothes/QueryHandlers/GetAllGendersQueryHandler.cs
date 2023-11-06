using MediatR;
using Swapy.BLL.Domain.Clothes.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Clothes.QueryHandlers
{
    public class GetAllGendersQueryHandler : IRequestHandler<GetAllGendersQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly IGenderRepository _genderRepository;

        public GetAllGendersQueryHandler(IGenderRepository genderRepository) => _genderRepository = genderRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllGendersQuery request, CancellationToken cancellationToken)
        {
            return await _genderRepository.GetAllSpecificationAsync();
        }
    }
}
