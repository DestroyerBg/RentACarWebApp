using System.ComponentModel.DataAnnotations;
using static RentACar.Common.Constants.DatabaseModelsConstants.Category;
namespace RentACar.Data.Models
{
    public class Category 
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Car> Cars { get; set; } = new HashSet<Car>();
    }
}
