using RentACar.DTO.InsuranceBenefit;

namespace RentACar.DTO.Car
{
    public class RentACarDTO
    {
        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public decimal PricePerDay { get; set; }

        public string City { get; set; } = null!;

        public ICollection<InsuranceBenefitDTO> Benefits { get; set; } = new HashSet<InsuranceBenefitDTO>();
    }
}
