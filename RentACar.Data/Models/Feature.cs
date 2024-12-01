using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models.Interfaces;
using static RentACar.Common.Constants.DatabaseModelsConstants.Feature;
namespace RentACar.Data.Models
{
    public class Feature : ISoftDeletable
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [Comment("Feature name")]
        [Unicode]
        public string Name { get; set; } = null!;

        public ICollection<CarFeature> CarFeatures { get; set; } = new HashSet<CarFeature>();
        public bool IsDeleted { get; set; }
    }
}
