using RentACar.Web.ViewModels.InsuranceBenefit;

namespace RentACar.Web.ViewModels.Reservation
{
    public class ReservationDetailsViewModel
    {
        public string? CarBrand { get; set; }
        public string? CarModel { get; set; }

        public string CarImageUrl { get; set; } = null!;
        public string StartDate { get; set; } = null!;
        public string EndDate { get; set; } = null!;
        public string? Address { get; set; }
        public decimal TotalPrice { get; set; }
        public string PhoneNumber { get; set; } = null!;

        public ICollection<InsuranceBenefitViewModel> InsuranceBenefits { get; set; } =
            new HashSet<InsuranceBenefitViewModel>();
    }
}
