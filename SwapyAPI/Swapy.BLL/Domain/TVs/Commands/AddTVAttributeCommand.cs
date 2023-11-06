using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.TVs.Commands
{
    public class AddTVAttributeCommand : AddProductCommand<TVAttribute>
    {
        public bool IsNew { get; set; }
        public bool IsSmart { get; set; }
        public string TVTypeId { get; set; }
        public string TVBrandId { get; set; }
        public string ScreenResolutionId { get; set; }
        public string ScreenDiagonalId { get; set; }
    }
}
