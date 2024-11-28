using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using RentACar.Common.ValidationAttributes;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
using static RentACar.Common.Constants.DatabaseModelsConstants.Car;
namespace RentACar.Web.ViewModels.Admin
{
    public class AddCarViewModel
    {
        [Required(ErrorMessage = FieldIsRequired)]
        [CustomStringLength(BrandMinLength, BrandMaxLength, AnyInputLengthErrorMessage)]
        public string Brand { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [CustomStringLength(ModelMinLength, ModelMaxLength, AnyInputLengthErrorMessage)]
        public string Model { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [YearValidation(YearOfManufactureMinValue)]
        public int YearOfManifacture { get; set; }

        [Required(ErrorMessage = FieldIsRequired)]
        [NumberValidation(HorsePowerMinValue, HorsePowerMaxValue)]
        public int HorsePower { get; set; }

        [Required(ErrorMessage = FieldIsRequired)]
        [RegularExpression(RegistrationNumberRegex)]
        public string RegistrationNumber { get; set; }

        [Required(ErrorMessage = FieldIsRequired)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = FieldIsRequired)]
        public int LocationId { get; set; }

        [Required(ErrorMessage = FieldIsRequired)]
        public decimal PricerPerDay { get; set; }

        [Required(ErrorMessage = FieldIsRequired)]
        public IFormFile CarImage { get; set; }
    }
}
