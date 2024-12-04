using System.ComponentModel.DataAnnotations;
using RentACar.Common.Constants;
using RentACar.Common.Messages;
using RentACar.Common.ValidationAttributes;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
using static RentACar.Common.Messages.DatabaseModelsMessages.Common;
namespace RentACar.Web.ViewModels.Account
{
    public class EditProfileViewModel
    {
        [Required(ErrorMessage = FieldIsRequired)]
        public string Id { get; set; } = null!;
        [Required]
        [EmailAddress]
        [Display(Name = "Имейл")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [Display(Name = "Потребителско име")]
        [CustomStringLength(UsernameNameMinLength, UsernameNameMaxLength, AnyInputLengthErrorMessage)]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [CustomStringLength(FirstNameMinLength, FirstNameMaxLength, AnyInputLengthErrorMessage)]
        [Display(Name = "Име")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [CustomStringLength(LastNameMinLength, LastNameMaxLength, AnyInputLengthErrorMessage)]
        [Display(Name = "Фамилно име")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [RegularExpression(PhoneNumberRegex, ErrorMessage = DatabaseModelsMessages.ApplicationUser.IncorrectPhoneNumberFormat)]
        [MaxLength(PhoneNumberLength)]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [DateValidation(DateFormat, DateIncorrectFormatErrorMessage)]
        public string BirthDate { get; set; } = null!;
    }
}
