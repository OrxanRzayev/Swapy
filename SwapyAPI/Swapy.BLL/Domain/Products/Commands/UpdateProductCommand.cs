using MediatR;
using Microsoft.AspNetCore.Http;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string CurrencyId { get; set; }
        public string CategoryId { get; set; }
        public string SubcategoryId { get; set; }
        public string CityId { get; set; }
        public List<string> OldPaths { get; set; }
        public IFormFileCollection NewFiles { get; set; }
    }
}
