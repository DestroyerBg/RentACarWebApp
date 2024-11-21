using RentACar.Web.ViewModels.InsuranceBenefit;

namespace RentACar.Web.ViewModels.Car
{
    public class ConfirmReservationViewModel
    {
        public string? CarBrand { get; set; }
        public string? CarModel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Address { get; set; }
        public decimal TotalPrice { get; set; }
        public string PhoneNumber { get; set; } = null!;

        public ICollection<InsuranceBenefitViewModel> InsuranceBenefits { get; set; } =
            new HashSet<InsuranceBenefitViewModel>();
    }
}
