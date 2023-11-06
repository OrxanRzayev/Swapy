namespace Swapy.Common.DTO.Clothes.Requests.Queries
{
    public class GetAllClothesViewsQueryDTO
    {
        public bool? IsChild { get; set; }
        public string? GenderId { get; set; }
        public string? ClothesTypeId { get; set; }
    }
}
