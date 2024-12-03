using Microsoft.AspNetCore.Mvc.Rendering;
using RentACar.Common.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
using static RentACar.Common.Constants.DatabaseModelsConstants.Car;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Web.ViewModels.ModelBinders;

namespace RentACar.Web.ViewModels.Admin
{
    [ModelBinder(typeof(EditCarModelBinder))]
    public class EditCarViewModel
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required(ErrorMessage = FieldIsRequired)]
        [CustomStringLength(BrandMinLength, BrandMaxLength, AnyInputLengthErrorMessage)]
        public string Brand { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [CustomStringLength(ModelMinLength, ModelMaxLength, AnyInputLengthErrorMessage)]
        public string Model { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [YearValidation(YearOfManufactureMinValue)]
        public int YearOfManufacture { get; set; }

        [Required(ErrorMessage = FieldIsRequired)]
        [NumberValidation(HorsePowerMinValue, HorsePowerMaxValue)]
        public int HorsePower { get; set; }

        [Required(ErrorMessage = FieldIsRequired)]
        [RegularExpression(RegistrationNumberRegex)]
        public string RegistrationNumber { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        public decimal PricePerDay { get; set; }
        public string? CarImageUrl { get; set; }

        public IFormFile? CarImage { get; set; }

        public ICollection<SelectListItem> Locations { get; set; } = new HashSet<SelectListItem>();

        public ICollection<SelectListItem> Categories { get; set; } = new HashSet<SelectListItem>();

        public ICollection<SelectListItem> Features { get; set; } = new HashSet<SelectListItem>();
    }
}
