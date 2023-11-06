using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Autos.Commands
{
    public class AddAutoAttributeCommand : AddProductCommand<AutoAttribute>
    {
        public int Miliage { get; set; }
        public int EngineCapacity { get; set; }
        public DateTime ReleaseYear { get; set; }
        public bool IsNew { get; set; }
        public string FuelTypeId { get; set; }
        public string AutoColorId { get; set; }
        public string TransmissionTypeId { get; set; }
        public string AutoModelId { get; set; }
    }
}
