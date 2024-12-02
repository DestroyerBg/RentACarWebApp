namespace RentACar.DTO.Car
{
    public class CarInformationDTO
    {
        public string Id { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int HorsePower { get; set; }
        public int YearOfManufacture { get; set; }
        public string City { get; set; } = null!;
        public string RegistrationNumber { get; set; } = null!;
        public bool IsHired { get; set; }


    }
}
