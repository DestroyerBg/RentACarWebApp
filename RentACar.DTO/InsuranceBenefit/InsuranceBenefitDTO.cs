using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentACar.DTO.InsuranceBenefit
{
    public class InsuranceBenefitDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public string IconClass { get; set; } = null!;

        public bool IsChecked = false;
    }
}
