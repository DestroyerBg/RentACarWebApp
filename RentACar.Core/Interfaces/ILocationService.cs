namespace RentACar.Core.Interfaces
{
    public interface ILocationService
    {
        Task<string?> GeocodeAsync(string address,string apiKey);
        Task<string?> ReverseGeocodeAsync(double latitude, double longitude, string apiKey);
    }
}
