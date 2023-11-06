using Swapy.Common.DTO.Products.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.Common.DTO.RealEstates.Responses
{
    public class RealEstateAttributesResponseDTO : ProductsResponseDTO<ProductResponseDTO>
    {
        public int MaxArea { get; set; }
        public int MinArea { get; set; }
        public int? MaxRooms { get; set; }
        public int? MinRooms { get; set; }

        public RealEstateAttributesResponseDTO(IEnumerable<ProductResponseDTO> items, int count, int allPages, decimal? maxPrice, decimal? minPrice, int maxArea, int minArea, int? maxRooms, int? minRooms) : base(items, count, allPages, maxPrice, minPrice)
        {
            MaxArea = maxArea;
            MinArea = minArea;
            MaxRooms = maxRooms;
            MinRooms = minRooms;
        }
    }
}
