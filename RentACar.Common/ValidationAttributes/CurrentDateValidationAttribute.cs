using System.ComponentModel.DataAnnotations;
using System.Globalization;
namespace RentACar.Common.ValidationAttributes
{
    public class CurrentDateValidationAttribute : ValidationAttribute
    {
        private readonly DateTime currDate = DateTime.Now.Date;
        private readonly string dateFormat;
        private readonly string errorMessage;
        public CurrentDateValidationAttribute(string _dateFormat, string _errorMessage)
        {
            dateFormat = _dateFormat;
            errorMessage = _errorMessage;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            bool isValid = DateTime.TryParseExact(value.ToString(), dateFormat, null, DateTimeStyles.None, out DateTime convertDate);
            if (!isValid)
            {
                string result = string.Format(errorMessage, validationContext.DisplayName);
                return new ValidationResult(ErrorMessage = errorMessage);
            }

            if (currDate > convertDate)
            {
                string result = string.Format(errorMessage, validationContext.DisplayName);
                return new ValidationResult(ErrorMessage = result);
            }

            return ValidationResult.Success;
        }
    }
}
