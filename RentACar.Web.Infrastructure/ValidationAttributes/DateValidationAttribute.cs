using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace RentACar.Web.Infrastructure.ValidationAttributes
{
    public class DateValidationAttribute : ValidationAttribute
    {
        private string dateFormat;
        private string errorMessage;
        public DateValidationAttribute(string _dateFormat, string _errorMessage)
        {
            dateFormat = _dateFormat;
            errorMessage = _errorMessage;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            bool isValid = DateTime.TryParseExact(value.ToString(), dateFormat, null, DateTimeStyles.None, out DateTime time);
            if (!isValid)
            {
                string result = string.Format(errorMessage, validationContext.DisplayName);
                return new ValidationResult(ErrorMessage = errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
