using System.ComponentModel.DataAnnotations;
using RentACar.Common.Constants;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;
namespace RentACar.Web.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = DatabaseModelsConstants.Common.FieldIsRequired)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = DatabaseModelsConstants.Common.FieldIsRequired)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
