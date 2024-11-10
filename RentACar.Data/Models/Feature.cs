using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static RentACar.Common.Constants.DatabaseModelsConstants.Feature;
namespace RentACar.Data.Models
{
    public class Feature
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(NameMaxLength)]
        [Comment("Feature name")]
        public string Name { get; set; } = null!;

        public ICollection<CarFeature> CarFeatures { get; set; } = new HashSet<CarFeature>();
    }
}
