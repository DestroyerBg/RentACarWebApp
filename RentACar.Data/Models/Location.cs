using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static RentACar.Common.Constants.DatabaseModelsConstants.Location;
namespace RentACar.Data.Models
{
    public class Location
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
    }
}
