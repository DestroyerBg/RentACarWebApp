using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;
namespace RentACar.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }
    }
}
