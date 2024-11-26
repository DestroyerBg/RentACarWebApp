using RentACar.DTO.InsuranceBenefit;

namespace RentACar.DTO.Reservation
{
    public class ReservationDetailsDTO
    {
        public string? CarBrand { get; set; }
        public string? CarModel { get; set; }
        public string StartDate { get; set; } = null!;
        public string EndDate { get; set; } = null!;

        public string CarImageUrl { get; set; } = null!;
        public string? Address { get; set; }
        public decimal TotalPrice { get; set; }
        public string PhoneNumber { get; set; } = null!;

        public ICollection<InsuranceBenefitDTO> InsuranceBenefits { get; set; } =
            new HashSet<InsuranceBenefitDTO>();
    }
}
