using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static RentACar.Common.Constants.DatabaseModelsConstants.Category;
namespace RentACar.Data.Models
{
    public class Category 
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [Comment("Category name")]
        public string Name { get; set; } = null!;

        public ICollection<Car> Cars { get; set; } = new HashSet<Car>();
    }
}
