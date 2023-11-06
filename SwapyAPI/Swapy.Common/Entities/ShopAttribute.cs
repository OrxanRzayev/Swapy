namespace Swapy.Common.Entities
{
    public class ShopAttribute
    {
        public string Id { get; set; }
        public string ShopName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Slogan { get; set; }
        public int Views { get; set; }
        public string Banner { get; set; }
        public string WorkDays { get; set; }
        public TimeSpan? StartWorkTime { get; set; }
        public TimeSpan? EndWorkTime { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public ShopAttribute() => Id = Guid.NewGuid().ToString();
    } 
}
