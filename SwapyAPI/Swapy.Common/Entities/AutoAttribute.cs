namespace Swapy.Common.Entities
{
    public class AutoAttribute
    {
        public string Id { get; set; }
        public int Miliage { get; set; }
        public int EngineCapacity { get;  set; }
        public DateTime ReleaseYear { get; set; }
        public bool IsNew { get; set; }
        public string FuelTypeId { get; set; }
        public FuelType FuelType { get; set; }
        public string AutoColorId { get; set; }
        public Color AutoColor { get; set; }
        public string TransmissionTypeId { get; set; }
        public TransmissionType TransmissionType { get; set; }
        public string AutoModelId { get; set; }
        public AutoModel AutoModel { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }

        public AutoAttribute() => Id = Guid.NewGuid().ToString();

        public AutoAttribute(int miliage, int engineCapacity, DateTime releaseYear, bool isNew, string fuelTypeId, string autoColorId, string transmissionTypeId, string autoModelId, string productId) : this()
        {
            Miliage = miliage;
            EngineCapacity = engineCapacity;
            ReleaseYear = releaseYear;
            IsNew = isNew;
            FuelTypeId = fuelTypeId;
            AutoColorId = autoColorId;
            TransmissionTypeId = transmissionTypeId;
            AutoModelId = autoModelId;
            ProductId = productId;
        }
    }
}
