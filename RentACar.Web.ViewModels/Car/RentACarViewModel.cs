using System.ComponentModel.DataAnnotations;
using RentACar.Common.ValidationAttributes;
using RentACar.Web.ViewModels.InsuranceBenefit;
using RentACar.Web.ViewModels.Location;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
using static RentACar.Common.Constants.DatabaseModelsConstants.Car;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;
using static RentACar.Common.Messages.DatabaseModelsMessages.Common;
using RentACar.Common.Constants;

namespace RentACar.Web.ViewModels.Car
{
    public class RentACarViewModel
    {
        public string Id { get; set; } = null!;

        public string? Brand { get; set; }

        public string? Model { get; set; }

        public decimal PricePerDay { get; set; }

        public string? City { get; set; }

        [Required(ErrorMessage = FieldIsRequired)]
        public string LocationId { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [DateValidation(DateFormat, DateIncorrectFormatErrorMessage)]
        [CurrentDateValidation(DateFormat,GivenDateShouldNotBeLowerThanToday)]
        [Display(Name= "Дата за наемане на МПС")]
        public string StartDate { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [DateValidation(DateFormat, DateIncorrectFormatErrorMessage)]
        [CurrentDateValidation(DateFormat, GivenDateShouldNotBeLowerThanToday)]
        [Display(Name = "Дата за връщане на МПС")]
        public string EndDate { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [CustomStringLength(AddressMinLength, DatabaseModelsConstants.Reservation.AddressMaxLength, AnyInputLengthErrorMessage)]
        [Display(Name = "Адрес")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [CustomStringLength(PhoneNumberLength, PhoneNumberLength, AnyInputLengthErrorMessage)]
        [RegularExpression(PhoneNumberRegex)]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; } = null!;

        public ICollection<InsuranceBenefitViewModel> Benefits { get; set; } = new HashSet<InsuranceBenefitViewModel>();

        public ICollection<LocationViewModel> Locations { get; set; } = new HashSet<LocationViewModel>();
    }
}
