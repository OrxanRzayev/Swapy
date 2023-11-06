using MediatR;
using Swapy.BLL.Domain.Clothes.Queries;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Clothes.QueryHandlers
{
    public class GetClothesBrandViewIdQueryHandler : IRequestHandler<GetClothesBrandViewIdQuery, string>
    {
        private readonly IClothesBrandViewRepository _clothesBrandViewRepository;

        public GetClothesBrandViewIdQueryHandler(IClothesBrandViewRepository clothesBrandViewRepository) => _clothesBrandViewRepository = clothesBrandViewRepository;

        public async Task<string> Handle(GetClothesBrandViewIdQuery request, CancellationToken cancellationToken)
        {
            return await _clothesBrandViewRepository.GetIdByBrandAndView(request.BrandId, request.ClothesViewId);
        }
    }
}
