namespace Swapy.Common.Entities
{
    public class ScreenDiagonal
    {
        public string Id { get; set; }
        public int Diagonal { get; set; }
        public ICollection<TVAttribute> TVAttributes { get; set; } = new List<TVAttribute>();

        public ScreenDiagonal() => Id = Guid.NewGuid().ToString();

        public ScreenDiagonal(int diagonal) : this() => Diagonal = diagonal;
    }
}
