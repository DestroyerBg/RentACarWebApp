using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static RentACar.Common.Constants.DatabaseModelsConstants.Insurance;
namespace RentACar.Data.Models
{
    public class Insurance
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(Reservation))]
        [Comment("Id of the reservation for which is the issurance")]
        public Guid ReservationId { get; set; }

        public Reservation Reservation { get; set; } = null!;

        [Required]
        [MaxLength(InsuranceProviderMaxLength)]
        [Comment("Insurance provider name")]
        public string InsuranceProvider { get; set; } = null!;

        [Required]
        [Column(TypeName = PricePrecision)]
        [Comment("This is a sum of the price from all insurance benefits")]
        public decimal InsuranceAmount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Comment("When the issurance expirates")]
        public DateTime ExpirationDate { get; set; }

        public ICollection<InsuranceBenefit> InsuranceBenefits { get; set; } = new HashSet<InsuranceBenefit>();

    }
}
