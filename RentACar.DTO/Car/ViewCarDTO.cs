namespace RentACar.DTO.Car
{
    public class ViewCarDTO
    {
        public Guid Id { get; set; }

        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int HorsePower { get; set; }

        public string RegistrationNumber { get; set; } = null!;

        public int YearOfManufacture { get; set; }

        public string Location { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal PricePerDay { get; set; }

        public ICollection<FeatureDTO> Features { get; set; } = new HashSet<FeatureDTO>();

    }
}
