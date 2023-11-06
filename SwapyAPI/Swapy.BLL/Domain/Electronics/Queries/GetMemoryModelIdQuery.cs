using MediatR;

namespace Swapy.BLL.Domain.Electronics.Queries
{
    public class GetMemoryModelIdQuery : IRequest<string>
    {
        public string MemoryId { get; set; }
        public string ModelId { get; set; }
    }
}
