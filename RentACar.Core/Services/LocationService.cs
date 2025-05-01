using System.Net;
using RentACar.Core.Infrastructure.GenericTypes;
using RentACar.Core.Interfaces;
using static RentACar.Common.Messages.ErrorMessages.GeolocationErrorMessages;

namespace RentACar.Core.Services
{
    public class LocationService : ILocationService
    {
        private readonly HttpClient httpClient;
        public LocationService(HttpClient _httpClient)
        {       
            httpClient = _httpClient;
        }
        public async Task<HttpResponseServiceResult<string>> ReverseGeocodeAsync(double latitude, double longitude)
        {
            string url = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "https://nominatim.openstreetmap.org/reverse?lat={0}&lon={1}&format=json",
                latitude, longitude);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.UserAgent.ParseAdd("RentACarApp/1.0 (rent-a-car@abv.bg)");

            HttpResponseMessage response = await httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                return HttpResponseServiceResult<string>.Success(content);
            }

            return HttpResponseServiceResult<string>.Failure(response.StatusCode, string.Format(ErrorWithExternalServiceForGeolocation, response.StatusCode));
        }
    }
}
