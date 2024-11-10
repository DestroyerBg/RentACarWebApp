using System.ComponentModel.DataAnnotations;
using RentACar.Data.Models.Interfaces;
using static RentACar.Common.Constants.DatabaseModelsConstants.Car;
namespace RentACar.Data.Models
{
    public class Car : ISoftDeletable
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(BrandMaxLength)]
        public string Brand { get; set; } = null!;

        [Required]
        [MaxLength(ModelMaxLength)]
        public string Model { get; set; } = null!;

        [Required]
        public int HorsePower { get; set; }

        [Required]
        [MaxLength(RegistrationNumberMaxLength)]
        public string RegistrationNumber { get; set; } = null!;

        [Required]
        public int YearOfManufacture { get; set; }

        [Required]
        public Category Category { get; set; } = null!;

        public ICollection<CarFeature> CarFeatures = new HashSet<CarFeature>();

        //public ICollection<Reservation> Reservations = new HashSet<Reservation>();
        public bool IsDeleted { get; set; } = false;
    }
}
