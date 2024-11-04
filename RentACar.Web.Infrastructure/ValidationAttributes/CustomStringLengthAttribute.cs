using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels.ValidationAttributes
{
    public class CustomStringLengthAttribute : ValidationAttribute
    {
        private readonly int minValue;
        private readonly int maxValue;
        private readonly string errorMessage;

        public CustomStringLengthAttribute(int minValue, int maxValue, string errorMessage)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.errorMessage = errorMessage;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value?.ToString().Length < minValue || value?.ToString().Length > maxValue)
            {
                string result = string.Format(errorMessage, validationContext.DisplayName, minValue, maxValue);
                return new ValidationResult(result);
            }

            return ValidationResult.Success;
        }
    }
}
