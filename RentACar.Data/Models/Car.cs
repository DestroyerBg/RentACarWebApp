using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey(nameof(Category))]
        [Comment("Category of the car")]
        public Guid CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Location))]
        [Comment("Car's location")]
        public Guid LocationId { get; set; }

        public Location Location { get; set; } = null!;

        [Required]
        [Comment("Car image url")]
        [MaxLength(CarImageUrlMaxlength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Column(TypeName = PricePrecision)]
        [Comment("Car rent price per day")]
        public decimal PricePerDay { get; set; }

        [Required]
        [Comment("Is car already hired")]
        public bool IsHired { get; set; } = false;

        public ICollection<CustomerFeedback> Comments { get; set; } = new HashSet<CustomerFeedback>();

        public ICollection<CarFeature> CarFeatures = new HashSet<CarFeature>();

        public ICollection<Reservation> Reservations = new HashSet<Reservation>();

        [Comment("Is the entity deleted?")]
        public bool IsDeleted { get; set; } = false;
    }
}
