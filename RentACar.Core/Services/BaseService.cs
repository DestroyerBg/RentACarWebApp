namespace RentACar.Core.Services
{
    public class BaseService
    {
        public bool IsValidGuid(string input)
        {
            bool isValid = Guid.TryParse(input, out Guid result);

            return isValid;
        }
    }
}
