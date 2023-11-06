using MediatR;

namespace Swapy.BLL.Domain.Electronics.Queries
{
    public class GetModelColorIdQuery : IRequest<string>
    {
        public string ColorId { get; set; }
        public string ModelId { get; set; }
    }
}
