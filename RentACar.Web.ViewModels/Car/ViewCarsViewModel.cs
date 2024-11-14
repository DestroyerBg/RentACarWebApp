namespace RentACar.Web.ViewModels.Car
{
    public class ViewCarsViewModel
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

        public ICollection<CarFeatureViewModel> Features { get; set; } = new HashSet<CarFeatureViewModel>();
    }
}
