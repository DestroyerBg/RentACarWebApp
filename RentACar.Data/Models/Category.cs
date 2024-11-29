using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models.Interfaces;
using static RentACar.Common.Constants.DatabaseModelsConstants.Category;
namespace RentACar.Data.Models
{
    public class Category : ISoftDeletable
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [Comment("Category name")]
        public string Name { get; set; } = null!;

        public ICollection<Car> Cars { get; set; } = new HashSet<Car>();
        public bool IsDeleted { get; set; }
    }
}
