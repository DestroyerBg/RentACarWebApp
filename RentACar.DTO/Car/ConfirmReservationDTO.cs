using RentACar.DTO.InsuranceBenefit;

namespace RentACar.DTO.Car
{
    public class ConfirmReservationDTO
    {
        public string? CarBrand { get; set; }
        public string? CarModel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public ICollection<InsuranceBenefitDTO> InsuranceBenefits { get; set; } = new HashSet<InsuranceBenefitDTO>();
        public decimal TotalPrice { get; set; }
    }
}
