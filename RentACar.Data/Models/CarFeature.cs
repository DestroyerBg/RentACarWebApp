using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RentACar.Data.Models.Interfaces;

namespace RentACar.Data.Models
{
    public class CarFeature : ISoftDeletable
    {
        [Required]
        [ForeignKey(nameof(Car))]
        public Guid CarId { get; set; }

        public Car Car { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Feature))]
        public Guid FeatureId { get; set; }

        public Feature Feature { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
