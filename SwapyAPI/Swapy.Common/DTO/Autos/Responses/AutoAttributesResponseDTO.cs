using Swapy.Common.DTO.Products.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.Common.DTO.Autos.Responses
{
    public class AutoAttributesResponseDTO : ProductsResponseDTO<ProductResponseDTO>
    {
        public int MaxMiliage { get; set; }
        public int MinMiliage { get; set; }
        public int MaxEngineCapacity { get; set; }
        public int MinEngineCapacity { get; set; }
        public int NewerReleaseYear { get; set; }
        public int OlderReleaseYear { get; set; }

        public AutoAttributesResponseDTO(IEnumerable<ProductResponseDTO> items, int count, int allPages, decimal? maxPrice, decimal? minPrice, int maxMiliage, int minMiliage, int maxEngineCapacity, int minEngineCapacity, int newerReleaseYear, int olderReleaseYear) : base(items, count, allPages, maxPrice, minPrice)
        {
            MaxMiliage = maxMiliage;
            MinMiliage = minMiliage;
            MaxEngineCapacity = maxEngineCapacity;
            MinEngineCapacity = minEngineCapacity;
            NewerReleaseYear = newerReleaseYear;
            OlderReleaseYear = olderReleaseYear;
        }
    }
}
