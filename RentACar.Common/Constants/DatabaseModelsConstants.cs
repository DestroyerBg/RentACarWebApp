﻿using System.Reflection.Metadata;

namespace RentACar.Common.Constants
{
    public static class DatabaseModelsConstants
    {
        public static class Common
        {
            public const string DateFormat = "dd.MM.yyyy";
            public const string DateIncorrectFormatErrorMessage =
                "Некоректен формат за {0}! Моля въведете валиден формат за {1}.";

            public const string AnyInputLengthErrorMessage = "{0} трябва да е с дължина между {1} и {2} символа.";

            public const string InternationalPhoneNumberRegex =
                @"^\+(1|2[0-9]|3[0-9]|4[0-9]|5[0-9]|6[0-9]|7[0-9]|8[0-9]|9[0-9])\d{4,14}$";
            public const string PhoneNumberRegex = @"^\+?([0-9]{1,3})?[-. ]?(\(?[0-9]{2,4}\)?)?[-. ]?[0-9]{3,4}[-. ]?[0-9]{3,4}$";
        }
        public static class ApplicationUser
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 20;
            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 20;

            public const int PhoneNumberLength = 10;
            public const string IncorrectPhoneNumberFormat = "Невалиден формат на телефонен номер.";
            public const int UsernameNameMinLength = 5;
            public const int UsernameNameMaxLength = 20;

            public const string PasswordsDoNotMatch = "Двете пароли не съвпадат.";
            public const string PasswordRequiresAtLeastOneDigit = "Задължително е паролата да съдържа поне 1 цифра.";

            public const string PasswordRequireAtLeastOneLowerCase =
                "Задължително е паролата да съдържа поне 1 малка буква.";
            public const string PasswordRequireAtLeastOneUpperCase =
                "Задължително е паролата да съдържа поне 1 главна буква.";

            public const string PasswordRequireAtLeastOneNonAlphaNumericCharacter = "Задължително е паролата да съдържа поне един уникален символ.";
            public const string PasswordTooShortMessage = "Паролата трябва да съдържа минимум {0} и максимум {1} знака";
            public const int PasswordMinlength = 8;
            public const int PasswordMaxLength = 100;
            public const string SuccessfullUpdatedProfile = "Успешно ъпдейтнахте профила си.";
            public const string CannotFindLoggedInUser = "Грешка при намирането на текущо логнатия потребител.";
            public const string NewPasswordIsDifferentThanOldPassword = "Паролите не съвпадат!";
            public const string ErrorWhenChangingPasswords = "Възникна грешка при смяна на паролата!";
            public const string ChangePasswordSuccess = "Смяната на паролата е успешна.";
            public const string UserWithThatUsernameExists = "Вече съществува потребител с това потребителско име.";
            public const string UserWithThatEmailExists = "Вече съществува потребител с този имейл.";
            public const string RegistrationSuccess = "Регистрацията е успешна";
            public const string FieldIsRequired = "{0} е задължително поле.";
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
            public const int AddressMaxLength = 100;
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
        }

        public static class Location
        {
            public const int CityMinLength = 2;
            public const int CityMaxLength = 50;
        }
    }
}
