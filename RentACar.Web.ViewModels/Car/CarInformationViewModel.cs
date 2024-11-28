namespace RentACar.Web.ViewModels.Car
{
    public class CarInformationViewModel
    {
        public string Id { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int HorsePower { get; set; }
        public int YearOfManufacture { get; set; }
        public string City { get; set; } = null!;
        public bool IsHired { get; set; }
    }
}
