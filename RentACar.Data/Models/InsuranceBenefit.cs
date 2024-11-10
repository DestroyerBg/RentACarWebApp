using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static RentACar.Common.Constants.DatabaseModelsConstants.InsuranceBenefit;
namespace RentACar.Data.Models
{
    public class InsuranceBenefit
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
    }
}