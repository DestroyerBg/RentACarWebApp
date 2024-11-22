using System.ComponentModel.DataAnnotations;
using RentACar.Common.ValidationAttributes;
using RentACar.Web.ViewModels.InsuranceBenefit;
using RentACar.Web.ViewModels.Location;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
using static RentACar.Common.Constants.DatabaseModelsConstants.Car;
using static RentACar.Common.Constants.DatabaseModelsConstants.Reservation;
using RentACar.Common.Constants;
namespace RentACar.Web.ViewModels.Car
{
    public class RentACarViewModel
    {
        public string Id { get; set; } = null!;

        public string? Brand { get; set; }

        public string? Model { get; set; }

        public decimal PricePerDay { get; set; }

        public string City { get; set; } = null!;

        [Required]
        public string LocationId { get; set; } = null!;

        [Required]
        [DateValidation(DateFormat, DateIncorrectFormatErrorMessage)]
        public string StartDate { get; set; } = null!;

        [Required]
        [DateValidation(DateFormat, DateIncorrectFormatErrorMessage)]
        public string EndDate { get; set; } = null!;

        [Required]
        [CustomStringLength(AddressMinLength, DatabaseModelsConstants.Reservation.AddressMaxLength, AnyInputLengthErrorMessage)]
        public string Address { get; set; } = null!;

        [Required]
        [CustomStringLength(PhoneNumberLength, PhoneNumberLength, AnyInputLengthErrorMessage)]
        [RegularExpression(PhoneNumberRegex)]
        public string PhoneNumber { get; set; } = null!;

        public ICollection<InsuranceBenefitViewModel> Benefits { get; set; } = new HashSet<InsuranceBenefitViewModel>();

        public ICollection<LocationViewModel> Locations { get; set; } = new HashSet<LocationViewModel>();
    }
}
