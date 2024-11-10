using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static RentACar.Common.Constants.DatabaseModelsConstants.CustomerFeedback;
namespace RentACar.Data.Models
{
    public class CustomerFeedback
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(Customer))]
        [Comment("Id of the customer")]
        public Guid CustomerId { get; set; }
        public ApplicationUser Customer { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Reservation))]
        [Comment("Id of the reservation")]
        public Guid ReservationId { get; set; }

        public Reservation Reservation { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Car))]
        [Comment("Id of the car")]
        public Guid CarId { get; set; }

        public Car Car { get; set; } = null!;

        [Required]
        [Comment("Stars given from user")]
        public double Rating { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        [Comment("Feedback description")]
        public string Description { get; set; } = null!;

        [Required]
        [DataType(DataType.DateTime)]
        [Comment("Date when comment was post")]
        public DateTime DateOfSubmission { get; set; }
    }
}
