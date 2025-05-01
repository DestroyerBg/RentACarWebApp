using RentACar.Core.Infrastructure.GenericTypes;

namespace RentACar.Core.Interfaces
{
    public interface ILocationService
    { 
        Task<HttpResponseServiceResult<string>> ReverseGeocodeAsync(double latitude, double longitude);
    }
}
