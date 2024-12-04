using System.ComponentModel.DataAnnotations;
using static RentACar.Common.Messages.DatabaseModelsMessages.Common;
namespace RentACar.Common.ValidationAttributes
{
    public class YearValidationAttribute : ValidationAttribute
    {
        private int yearMinValue;
        private int yearMaxValue = DateTime.Now.Year;

        public YearValidationAttribute(int _yearMinValue)
        {
            yearMinValue = _yearMinValue;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(NullYearValidation);
            }

            bool tryParseValue = int.TryParse(value.ToString(), out int currentYear);

            if (!tryParseValue)
            {
                return new ValidationResult(YearShouldBeANumber);
            }

            if (currentYear <= yearMaxValue && currentYear >= yearMinValue)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(YearRangeError);
        }
    }
}
