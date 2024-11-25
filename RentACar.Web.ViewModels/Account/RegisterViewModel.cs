using System.ComponentModel.DataAnnotations;
using RentACar.Common.ValidationAttributes;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
namespace RentACar.Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = FieldIsRequired)]
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
        [RegularExpression(PhoneNumberRegex, ErrorMessage = IncorrectPhoneNumberFormat)]
        [MaxLength(PhoneNumberLength)]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; } = null!;
        [Required(ErrorMessage = FieldIsRequired)]
        [DateValidation(DateFormat, DateIncorrectFormatErrorMessage)]
        [Display(Name = "Рожденна дата")]
        public string BirthDate { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [CustomStringLength(PasswordMinlength, PasswordMaxLength, AnyInputLengthErrorMessage)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [DataType(DataType.Password)]
        [Display(Name = "Повторете паролата.")]
        [Compare("Password", ErrorMessage = PasswordsDoNotMatch)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
