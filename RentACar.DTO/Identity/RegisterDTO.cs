using System.ComponentModel.DataAnnotations;
using RentACar.Common.Constants;
using RentACar.Common.Messages;
using RentACar.Common.ValidationAttributes;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
using static RentACar.Common.Messages.DatabaseModelsMessages.ApplicationUser;
using static RentACar.Common.Messages.DatabaseModelsMessages.Common;
namespace RentACar.DTO.Identity
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [CustomStringLength(UsernameNameMinLength, UsernameNameMaxLength, AnyInputLengthErrorMessage)]
        public string Username { get; set; } = null!;

        [Required]
        [CustomStringLength(FirstNameMinLength, FirstNameMaxLength, AnyInputLengthErrorMessage)]
        public string FirstName { get; set; } = null!;

        [Required]
        [CustomStringLength(LastNameMinLength, LastNameMaxLength, AnyInputLengthErrorMessage)]
        public string LastName { get; set; } = null!;

        [Required]
        [RegularExpression(PhoneNumberRegex, ErrorMessage = IncorrectPhoneNumberFormat)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [DateValidation(DateFormat, DateIncorrectFormatErrorMessage)]
        public string BirthDate { get; set; } = null!;

        [Required]
        [StringLength(PasswordMaxLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = PasswordsDoNotMatch)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
