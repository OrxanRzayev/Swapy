using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;

namespace Swapy.Common.DTO.Categories.Responses
{
    public class CategoryTreeResponseDTO
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public CategoryType Type { get; set; }
        public SubcategoryType? SubType { get; set; }
        public bool IsFinal { get; set; }
        public SpecificationResponseDTO<string> Parent { get; set; }

        public CategoryTreeResponseDTO() { }

        public CategoryTreeResponseDTO(string id, CategoryType type, SubcategoryType? subType, string value, bool isFinal, string parentId, string parentName)
        {
            Id = id;
            Type = type;
            SubType = subType;
            Value = value;
            IsFinal = isFinal;
            Parent = new SpecificationResponseDTO<string>(parentId, parentName);
        }
    }
}
