﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using RentACar.Common.ValidationAttributes;
using RentACar.Web.ViewModels.Category;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
using static RentACar.Common.Constants.DatabaseModelsConstants.Car;
using RentACar.Web.ViewModels.Location;
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
        public string RegistrationNumber { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        public string CategoryId { get; set; }

        [Required(ErrorMessage = FieldIsRequired)]
        public string LocationId { get; set; }

        [Required(ErrorMessage = FieldIsRequired)]
        public decimal PricePerDay { get; set; }

        public string? CarImageUrl { get; set; }

        public IFormFile? CarImage { get; set; }

        public ICollection<LocationViewModel> Locations { get; set; } = new HashSet<LocationViewModel>();

        public ICollection<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();
    }
}
