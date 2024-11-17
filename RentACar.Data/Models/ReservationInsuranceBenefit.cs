using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentACar.Data.Models
{
    public class ReservationInsuranceBenefit
    {
        [Required]
        [ForeignKey(nameof(Reservation))]
        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(InsuranceBenefit))]
        public Guid InsuranceBenefitId { get; set; }

        public InsuranceBenefit InsuranceBenefit { get; set; } = null!;
    }
}
