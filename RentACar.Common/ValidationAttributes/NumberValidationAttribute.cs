using System.ComponentModel.DataAnnotations;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
namespace RentACar.Common.ValidationAttributes
{
    public class NumberValidationAttribute : ValidationAttribute
    {
        private readonly int numberMinValue;
        private readonly int numberMaxValue;

        public NumberValidationAttribute(int _numberMinValue, int _numberMaxValue)
        {
            numberMinValue = _numberMinValue;
            numberMaxValue = _numberMaxValue;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || !int.TryParse(value.ToString(), out int result))
            {
                return new ValidationResult(string.Format(NumberShouldBeInARange, numberMinValue, numberMaxValue));
            }

            if (result <= numberMaxValue && result >= numberMinValue) 
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(string.Format(NumberShouldBeInARange, numberMinValue, numberMaxValue));
        }
    }
}
