using Swapy.Common.DTO.Products.Responses;

namespace Swapy.Common.DTO.TVs.Responses
{
    public class TVAttributeResponseDTO : AttributeResponseDTO
    {
        public bool IsNew { get; set; }
        public bool IsSmart { get; set; }
        public string TVBrandId { get; set; }
        public string TVBrand { get; set; }
        public string TVTypeId { get; set; }
        public string TVType { get; set; }
        public int ScreenDiagonal { get; set; }
        public string ScreenDiagonalId { get; set; }
        public string ScreenResolutionId { get; set; }
        public string ScreenResolution { get; set; }
    }
}
