using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models.Interfaces;
using static RentACar.Common.Constants.DatabaseModelsConstants.Location;
namespace RentACar.Data.Models
{
    public class Location : ISoftDeletable
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(CityMaxLength)]
        [Comment("City name")]
        public string City { get; set; } = null!;
        
        [Required]
        [Comment("City's postal code")]
        public int PostalCode { get; set; }

        public ICollection<Car> Cars { get; set; } = new HashSet<Car>();

        public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
        public bool IsDeleted { get; set; } = false;
    }
}
