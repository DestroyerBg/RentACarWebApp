using Microsoft.AspNetCore.Mvc;
using RentACar.Core.Interfaces;
using static RentACar.Common.Messages.ErrorMessages.GeolocationErrorMessages;
namespace RentACar.Web.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class GeolocationController : ControllerBase
    {
        private readonly ILocationService locationService;
        private readonly IStringProvider stringProvider;

        public GeolocationController(
            ILocationService _locationService, 
            IStringProvider _stringProvider)
        {
            locationService = _locationService;
            stringProvider = _stringProvider;
        }

        [HttpGet("geocode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Geocode(string address)
        {
            string apiKey = stringProvider.GetGeolocationApiKey();
            string? result = await locationService.GeocodeAsync(address, apiKey);
            if (string.IsNullOrEmpty(result))
            {
                return NotFound(EmptyAddressGiven);
            }
            return Ok(result);
        }

        [HttpGet("reverse-geocode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReverseGeocode(double latitude, double longitude)
        {
            string apiKey = stringProvider.GetGeolocationApiKey();
            string? result = await locationService.ReverseGeocodeAsync(latitude, longitude, apiKey);
            if (string.IsNullOrEmpty(result))
            {
                return NotFound(DidNotFindInforForCordinates);
            }
            return Ok(result);
        }


    }
}
