using RentACar.DTO.InsuranceBenefit;
using RentACar.Web.ViewModels.InsuranceBenefit;
using RentACar.Web.ViewModels.Location;

namespace RentACar.Web.ViewModels.Car
{
    public class RentACarViewModel
    {
        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public decimal PricePerDay { get; set; }

        public string City { get; set; } = null!;

        public string StartDate { get; set; } = null!;

        public string EndDate { get; set; } = null!;

        public string Address { get; set; } = null!;

        public ICollection<InsuranceBenefitViewModel> Benefits { get; set; } = new HashSet<InsuranceBenefitViewModel>();

        public ICollection<LocationViewModel> Locations { get; set; } = new HashSet<LocationViewModel>();
    }
}
