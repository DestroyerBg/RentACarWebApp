using RentACar.DTO.InsuranceBenefit;

namespace RentACar.DTO.Reservation
{
    public class CreateReservationDTO
    {
        public Guid CarId { get; set; }

        public string StartDate { get; set; } = null!;

        public string EndDate { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string LocationId { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
        public decimal TotalPrice { get; set; }

        public ICollection<InsuranceBenefitDTO> InsuranceBenefits { get; set; } = new HashSet<InsuranceBenefitDTO>();
    }
}
