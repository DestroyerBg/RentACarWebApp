using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models.Interfaces;
using static RentACar.Common.Constants.DatabaseModelsConstants.Reservation;
namespace RentACar.Data.Models
{
    public class Reservation :ISoftDeletable
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(Customer))]
        [Comment("This is customer id")]
        public Guid CustomerId { get; set; }
        public ApplicationUser Customer { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Car))]
        [Comment("This is car id")]
        public Guid CarId { get; set; }

        public Car Car { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        [Comment("When the reservation begins")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Comment("When the reservation ends")]
        public DateTime EndDate { get; set; }

        [Required]
        [Column(TypeName = PricePrecision)]
        [Comment("Total price for the reservation")]
        public decimal TotalPrice { get; set; }

        [Comment("Is the entity deleted?")]
        public bool IsDeleted { get; set; } = false;
    }
}
