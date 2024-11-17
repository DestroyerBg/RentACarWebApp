using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models.Interfaces;
using static RentACar.Common.Constants.DatabaseModelsConstants.InsuranceBenefit;
namespace RentACar.Data.Models
{
    public class InsuranceBenefit : ISoftDeletable
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(InsuranceBenefitMaxLength)]
        [Comment("Issurance benefit name")]
        public string Name { get; set; } = null!;

        [Required]
        [Column(TypeName = PricePrecision)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(IconClassMaxLength)]
        [Comment("Icon font-awesome class which is used for front-end")] 
        public string IconClass { get; set; } = null!;

        public ICollection<ReservationInsuranceBenefit> Reservations { get; set; } = new HashSet<ReservationInsuranceBenefit>();
        public bool IsDeleted { get; set; } = false;
    }
}