using System.ComponentModel.DataAnnotations;
using RentACar.Common.Constants;
using RentACar.Common.ValidationAttributes;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
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
        [CustomStringLength(UsernameNameMinLength, UsernameNameMaxLength, DatabaseModelsConstants.Common.AnyInputLengthErrorMessage)]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [CustomStringLength(FirstNameMinLength, FirstNameMaxLength, DatabaseModelsConstants.Common.AnyInputLengthErrorMessage)]
        [Display(Name = "Име")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [CustomStringLength(LastNameMinLength, LastNameMaxLength, DatabaseModelsConstants.Common.AnyInputLengthErrorMessage)]
        [Display(Name = "Фамилно име")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [RegularExpression(PhoneNumberRegex, ErrorMessage = IncorrectPhoneNumberFormat)]
        [MaxLength(PhoneNumberLength)]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [DateValidation(DatabaseModelsConstants.Common.DateFormat, DateIncorrectFormatErrorMessage)]
        public string BirthDate { get; set; } = null!;
    }
}
