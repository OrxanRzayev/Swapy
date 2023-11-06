namespace Swapy.Common.DTO.Products.Responses
{
    public class CurrencyResponseDTO : SpecificationResponseDTO<string>
    {
        public string Symbol { get; set; }

        public CurrencyResponseDTO() : base() { }

        public CurrencyResponseDTO(string id, string name, string symbol) : base(id, name) => Symbol = symbol;
    }
}
