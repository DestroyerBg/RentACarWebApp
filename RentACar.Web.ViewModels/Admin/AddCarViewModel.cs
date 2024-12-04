using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using RentACar.Common.ValidationAttributes;
using RentACar.Web.ViewModels.Category;
using static RentACar.Common.Constants.DatabaseModelsConstants.Car;
using static RentACar.Common.Messages.DatabaseModelsMessages.Common;
using RentACar.Web.ViewModels.Location;
using Microsoft.AspNetCore.Mvc;
using RentACar.Web.ViewModels.Feature;
using RentACar.Web.ViewModels.ModelBinders;

namespace RentACar.Web.ViewModels.Admin
{
    [ModelBinder(BinderType = typeof(AddCarModelBinder))]
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
        public int YearOfManufacture { get; set; }

        [Required(ErrorMessage = FieldIsRequired)]
        [NumberValidation(HorsePowerMinValue, HorsePowerMaxValue)]
        public int HorsePower { get; set; }

        [Required(ErrorMessage = FieldIsRequired)]
        public string RegistrationNumber { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        public string CategoryId { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        public string LocationId { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        public decimal PricePerDay { get; set; }

        public string? CarImageUrl { get; set; }

        public IFormFile? CarImage { get; set; }

        public ICollection<LocationViewModel> Locations { get; set; } = new HashSet<LocationViewModel>();

        public ICollection<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();

        public ICollection<FeatureCheckboxViewModel> Features { get; set; } = new HashSet<FeatureCheckboxViewModel>();
    }
}
