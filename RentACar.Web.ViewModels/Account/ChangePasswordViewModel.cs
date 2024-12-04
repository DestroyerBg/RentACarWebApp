using System.ComponentModel.DataAnnotations;
using static RentACar.Common.Messages.DatabaseModelsMessages.Common;
using static RentACar.Common.Messages.DatabaseModelsMessages.ApplicationUser;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;

namespace RentACar.Web.ViewModels.Account
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = FieldIsRequired)]
        [Display(Name = "Стара парола")]
        public string OldPassword { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [StringLength(PasswordMaxLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string NewPassword { get; set; } = null!;


        [Required(ErrorMessage = FieldIsRequired)]
        [DataType(DataType.Password)]
        [Display(Name = "Повторете паролата.")]
        [Compare("NewPassword", ErrorMessage = PasswordsDoNotMatch)]
        public string ConfirmPassword { get; set;} = null!;
    }
}
