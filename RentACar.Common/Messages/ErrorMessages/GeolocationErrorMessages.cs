namespace RentACar.Common.Messages.ErrorMessages
{
    public static class GeolocationErrorMessages
    {
        public const string EmptyAddressGiven = "Адресът не може да бъде празен.";
        public const string DidNotFindInfoForAddress = "Не е намерена информация за този адрес.";
        public const string DidNotFindInforForCordinates = "Не е намерена информация за тeзи кординати.";

        public const string ErrorWithExternalServiceForGeolocation =
            "Услугите, която използваме за да изчислим адреса временно не работи. Status code = {0}";
    }
}
