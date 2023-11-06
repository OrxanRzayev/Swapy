using MediatR;
using Swapy.BLL.Domain.Clothes.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Clothes.QueryHandlers
{
    public class GetAllClothesBrandsQueryHandler : IRequestHandler<GetAllClothesBrandsQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly IClothesBrandRepository _clothesBrandRepository;

        public GetAllClothesBrandsQueryHandler(IClothesBrandRepository clothesBrandRepository) => _clothesBrandRepository = clothesBrandRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllClothesBrandsQuery request, CancellationToken cancellationToken)
        {
            var result = (await _clothesBrandRepository.GetByClothesViewsAsync(request.ClothesViewsId)).Select(x => new SpecificationResponseDTO<string>(x.Id, x.Name));
            return result;
        }
    }
}
