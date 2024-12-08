namespace RentACar.Common.Constants
{
    public static class DatabaseModelsConstants
    {
        public static class Common
        {
            public const string DateFormat = "dd.MM.yyyy";
            public const string InternationalPhoneNumberRegex =
                @"^\+(1|2[0-9]|3[0-9]|4[0-9]|5[0-9]|6[0-9]|7[0-9]|8[0-9]|9[0-9])\d{4,14}$";
            public const string PhoneNumberRegex = @"^\+?([0-9]{1,3})?[-. ]?(\(?[0-9]{2,4}\)?)?[-. ]?[0-9]{3,4}[-. ]?[0-9]{3,4}$";
            public const string NoImageUrl = "~/images/cars/no-image.jpg";
            public const string UniqueDateFormat = "yyyyMMddHHmmss";
            public const string SuccessfullMessageString = "Successfull";
            public const string ErrorMessageString = "Error";
        }
        public static class ApplicationUser
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 20;
            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 20;

            public const int PhoneNumberLength = 10;
            public const int UsernameNameMinLength = 5;
            public const int UsernameNameMaxLength = 20;
            public const string StardardUserRoleName = "User";
            public const string ModeratorRoleName = "Moderator";
            public const string AdminRoleName = "Admin";
            public const int PasswordMinlength = 8;
            public const int PasswordMaxLength = 100;
            public const string SuperAdminClaimType = "SuperAdmin";
            public const string SuperAdminClaimValue = "true";

        }

        public static class Car
        {
            public const int BrandMinLength = 2;
            public const int BrandMaxLength = 20;

            public const int ModelMinLength = 2;
            public const int ModelMaxLength = 10;

            public const int HorsePowerMinValue = 60;
            public const int HorsePowerMaxValue = 5000;

            public const string RegistrationNumberRegex = "[A-Я]{1}[0-9]{4}[А-Я]{2}";
            public const int RegistrationNumberMaxLength = 7;

            public const int YearOfManufactureMinValue = 1960;

            public const int CarImageUrlMaxlength = 100;

            public const string PricePrecision = "decimal(18, 2)";

            public const int AddressMinLength = 4;
            public const int AddressMaxLength = 100;

            public const int MaxPhotoFileSize = 1_048_576_00;

        }

        public static class Feature
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 100;
        }

        public static class Category
        {
            public const int NameMinlength = 3;
            public const int NameMaxLength = 20;
        }

        public static class Reservation
        {
            public const string PricePrecision = "decimal(18, 2)";
            public const string OrderPrecision = "decimal(20, 0)";
            public const int AddressMaxLength = 100;
            public const int OrderNumberStringMaxLength = 100;
        }

        public static class Insurance
        {
            public const int InsuranceProviderMinLength = 3;
            public const int InsuranceProviderMaxLength = 20;
            public const string PricePrecision = "decimal(18, 2)";
        }

        public static class InsuranceBenefit
        {
            public const int InsuranceBenefitMinLength = 3;
            public const int InsuranceBenefitMaxLength = 100;
            public const string PricePrecision = "decimal(18, 2)";
            public const int IconClassMaxLength = 100;
        }

        public static class CustomerFeedback
        {
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 3000;
            public const int RatingMinValue = 1;
            public const int RatingMaxValue = 5;
            public const string ShowDeleteOptionString = "ShowDeleteOptionsWithJS";
        }

        public static class Location
        {
            public const int CityMinLength = 2;
            public const int CityMaxLength = 50;
        }
    }
}
