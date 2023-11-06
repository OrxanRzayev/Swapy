using Swapy.Common.Enums;

namespace Swapy.Common.DTO.Products.Responses
{
    public class ProductSubcategoryResponseDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public CategoryType Type { get; set; }
        public string CategoryId { get; set; }
        public SubcategoryType? SubType { get; set; }

        public ProductSubcategoryResponseDTO(string id, string name, CategoryType type, string categoryId, SubcategoryType? subType)
        {
            Id = id;
            Name = name;
            Type = type;
            CategoryId = categoryId;
            SubType = subType;
        }
    }
}
