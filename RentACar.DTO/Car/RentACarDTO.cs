using RentACar.DTO.InsuranceBenefit;
using RentACar.DTO.Location;

namespace RentACar.DTO.Car
{
    public class RentACarDTO
    {
        public Guid Id { get; set; }
        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public decimal PricePerDay { get; set; }

        public string City { get; set; } = null!;

        public ICollection<InsuranceBenefitDTO> Benefits { get; set; } = new HashSet<InsuranceBenefitDTO>();

        public ICollection<LocationDTO> Locations { get; set; } = new HashSet<LocationDTO>();
    }
}
