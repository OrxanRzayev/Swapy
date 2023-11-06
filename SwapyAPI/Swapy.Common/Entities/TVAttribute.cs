namespace Swapy.Common.Entities
{
    public class TVAttribute
    {
        public string Id { get; set; }  
        public bool IsNew { get; set; }
        public bool IsSmart { get; set; }
        public string TVTypeId { get; set; }
        public TVType TVType { get; set; }
        public string TVBrandId { get; set; }
        public TVBrand TVBrand { get; set; }
        public string ScreenResolutionId { get; set; } 
        public ScreenResolution ScreenResolution { get; set; }
        public string ScreenDiagonalId { get; set; }
        public ScreenDiagonal ScreenDiagonal { get; set; }  
        public string ProductId { get; set; }
        public Product Product { get; set; }

        public TVAttribute() => Id = Guid.NewGuid().ToString();

        public TVAttribute(bool isNew, bool isSmart, string tvTypeId, string tvBrandId, string screenResolutionId, string screenDiagonalId, string productId) : this()
        {  
            IsNew = isNew;
            IsSmart = isSmart; 
            TVTypeId = tvTypeId;
            TVBrandId = tvBrandId;
            ScreenResolutionId = screenResolutionId;
            ScreenDiagonalId = screenDiagonalId;  
            ProductId = productId;
        }  
    }
}
