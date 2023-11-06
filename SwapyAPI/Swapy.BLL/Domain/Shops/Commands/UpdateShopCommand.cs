using MediatR;

namespace Swapy.BLL.Domain.Shops.Commands
{
    public class UpdateShopCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public string ShopName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Slogan { get; set; }
        public string Banner { get; set; }
        public string WorkDays { get; set; }
        public TimeSpan? StartWorkTime { get; set; }
        public TimeSpan? EndWorkTime { get; set; }
    }
}
