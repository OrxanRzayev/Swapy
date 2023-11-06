using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllCurrenciesQueryHandler : IRequestHandler<GetAllCurrenciesQuery, IEnumerable<CurrencyResponseDTO>>
    {
        private readonly ICurrencyRepository _currencyRepository;

        public GetAllCurrenciesQueryHandler(ICurrencyRepository currencyRepository) => _currencyRepository = currencyRepository;

        public async Task<IEnumerable<CurrencyResponseDTO>> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
        {
            var result = (await _currencyRepository.GetAllAsync()).Select(x => new CurrencyResponseDTO(x.Id, x.Name, x.Symbol));
            return result;
        }
    }
}
