using System.Text.Json;
using RentACar.Core.Interfaces;
using RentACar.DTO.Location;

namespace RentACar.Core.Services
{
    public class LocationService : ILocationService
    {
        private readonly HttpClient httpClient;
        private readonly IStringProvider stringProvider;

        public LocationService(HttpClient _httpClient, 
            IStringProvider _stringProvider)
        {
            httpClient = _httpClient;
            stringProvider = _stringProvider;
        }
        public async Task<string?> GeocodeAsync(string address, string apiKey)
        {
            string url = $"https://api.opencagedata.com/geocode/v1/json?q={Uri.EscapeDataString(address)}&key={apiKey}";

            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            OpenCageResponseDTO? data = JsonSerializer.Deserialize<OpenCageResponseDTO>(json, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return data?.Results?.FirstOrDefault()?.Formatted;
        }

        public async Task<string?> ReverseGeocodeAsync(double latitude, double longitude, string apiKey)
        {
            string url = $"https://api.opencagedata.com/geocode/v1/json?q={latitude},{longitude}&key={apiKey}&language=bg";

            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            OpenCageResponseDTO? data = JsonSerializer.Deserialize<OpenCageResponseDTO>(json,new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return data?.Results?.FirstOrDefault()?.Formatted;
        }
    }
}
