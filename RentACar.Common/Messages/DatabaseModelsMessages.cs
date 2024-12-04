namespace RentACar.Common.Messages
{
    public static class DatabaseModelsMessages
    {
        public static class Common
        {
            public const string DateIncorrectFormatErrorMessage =
                "Некоректен формат за {0}! Моля въведете валиден формат за {1}.";

            public const string AnyInputLengthErrorMessage = "{0} трябва да е с дължина между {1} и {2} символа.";
            public const string FieldIsRequired = "{0} е задължително поле.";
            public const string YearRangeError = "Годината трябва да съдържа стойност между {0} и {1}";
            public const string NullYearValidation = "Годината не може да бъде null.";
            public const string YearShouldBeANumber = "Годината трябва да е число.";
            public const string NumberShouldBeInARange = "{0} трябва да е число в диапазона {1}-{2}";
            public const string UploadPhotoError = "Снимката не можа да се качи успешно. Опитай пак!";
            public const string InvalidGuidId = "Невалидно Id";
        }

        public static class ApplicationUser
        {
            public const string IncorrectPhoneNumberFormat = "Невалиден формат на телефонен номер.";
            public const string PasswordsDoNotMatch = "Двете пароли не съвпадат.";
            public const string PasswordRequiresAtLeastOneDigit = "Задължително е паролата да съдържа поне 1 цифра.";
            public const string PasswordRequireAtLeastOneLowerCase =
                "Задължително е паролата да съдържа поне 1 малка буква.";
            public const string PasswordRequireAtLeastOneUpperCase =
                "Задължително е паролата да съдържа поне 1 главна буква.";

            public const string PasswordRequireAtLeastOneNonAlphaNumericCharacter = "Задължително е паролата да съдържа поне един уникален символ.";
            public const string PasswordTooShortMessage = "Паролата трябва да съдържа минимум {0} и максимум {1} знака";
            public const string SuccessfullUpdatedProfile = "Успешно ъпдейтнахте профила си.";
            public const string CannotFindLoggedInUser = "Грешка при намирането на текущо логнатия потребител.";
            public const string NewPasswordIsDifferentThanOldPassword = "Паролите не съвпадат!";
            public const string ErrorWhenChangingPasswords = "Възникна грешка при смяна на паролата!";
            public const string ChangePasswordSuccess = "Смяната на паролата е успешна.";
            public const string UserWithThatUsernameExists = "Вече съществува потребител с това потребителско име.";
            public const string UserWithThatEmailExists = "Вече съществува потребител с този имейл.";
            public const string RegistrationSuccess = "Регистрацията е успешна";
        }
        public static class Car
        {
            public const string CarWithThatRegistrationNumberExists = "Вече е добавена кола с този регистрационен номер.";

            public const string ErrorWhenAddingCar = "Възникна грешка при добавяне на колата.";
            public const string CarAddedSuccessfully = "Колата е добавена успешно!";
            public const string CarDeletionError = "Възникна грешка при изтриването на колата.";
            public const string CarDeletedSuccessfully = "Колата беше изтрита успешно";
            public const string InvalidCarId = "Няма кола с това Id";
            public const string ProvidedRegistrationNumberAlreadyIsOnAnotherCar =
                "Подаденият регистрационен вече е наличен в приложението на друга кола.";

            public const string ErrorWhenEditCar = "Възникна грешка при записването на промените по колата.";
            public const string EditCarSuccessfull = "Промените са записани успешно.";
        }
    }
}
