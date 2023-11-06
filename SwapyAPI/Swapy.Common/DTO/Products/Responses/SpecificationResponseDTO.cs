namespace Swapy.Common.DTO.Products.Responses
{
    public class SpecificationResponseDTO<T>
    {
        public string Id { get; set; }
        public T Value { get; set; }

        public SpecificationResponseDTO() { }

        public SpecificationResponseDTO(string id, T value)
        {
            Id = id;
            Value = value;
        }
    }
}
