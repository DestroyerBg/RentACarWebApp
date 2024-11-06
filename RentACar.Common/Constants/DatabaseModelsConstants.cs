using System.Data;

namespace RentACar.Common.Constants
{
    public static class DatabaseModelsConstants
    {
        public static class ApplicationUser
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 20;
            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 20;
            public const string AnyInputLengthErrorMessage = "{0} трябва да е с дължина между {1} и {2} символа.";
            public const string PhoneNumberRegex =
                @"^\+?([0-9]{1,3})?[-. ]?(\(?[0-9]{2,4}\)?)?[-. ]?[0-9]{3,4}[-. ]?[0-9]{3,4}$";

            public const int PhoneNumberLength = 10;
            public const string IncorrectPhoneNumberFormat = "Невалиден формат на телефонен номер.";
            public const int UsernameNameMinLength = 5;
            public const int UsernameNameMaxLength = 20;
            public const string DateFormat = "dd.MM.yyyy";
            public const string DateIncorrectFormatErrorMessage =
                "Некоректен формат за {0}! Моля въведете валиден формат за {1}.";

            public const string PasswordsDoNotMatch = "Двете пароли не съвпадат.";
            public const string PasswordRequiresAtLeastOneDigit = "Задължително е паролата да съдържа поне 1 цифра.";
            public const int PasswordMaxLength = 100;
        }
    }
}
