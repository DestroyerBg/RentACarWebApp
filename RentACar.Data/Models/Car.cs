using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
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
        [Comment("E.G. BMW, Mercedes or etc")]
        public string Brand { get; set; } = null!;

        [Required]
        [MaxLength(ModelMaxLength)]
        [Comment("E.G. model number like E36, E60 or w211")]
        public string Model { get; set; } = null!;

        [Required]
        [Comment("Car's horsepower")]
        public int HorsePower { get; set; }

        [Required]
        [MaxLength(RegistrationNumberMaxLength)]
        [Comment("Car's registration number")]
        public string RegistrationNumber { get; set; } = null!;

        [Required]
        [Comment("Year when car was produced")]
        public int YearOfManufacture { get; set; }

        [Required]
        [Comment("Category of the car")]
        public Category Category { get; set; } = null!;

        public ICollection<CarFeature> CarFeatures = new HashSet<CarFeature>();

        public ICollection<Reservation> Reservations = new HashSet<Reservation>();

        [Comment("Is the entity deleted?")]
        public bool IsDeleted { get; set; } = false;
    }
}
