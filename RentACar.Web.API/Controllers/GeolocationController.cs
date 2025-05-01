using System.Net;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.Infrastructure.GenericTypes;
using RentACar.Core.Interfaces;
using static RentACar.Common.Messages.ErrorMessages.GeolocationErrorMessages;
namespace RentACar.Web.API.Controllers
{
    public class GeolocationController : BaseApiController
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

        [HttpGet("reverse-geocode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> ReverseGeocode(double latitude, double longitude)
        { 
            HttpResponseServiceResult<string> result = await locationService.ReverseGeocodeAsync(latitude, longitude);

            if (result.StatusCode != HttpStatusCode.OK)
            {
               return StatusCode((int)result.StatusCode, String.Format(ErrorWithExternalServiceForGeolocation, result.StatusCode));
            }

            return Ok(result.Data);
        }


    }
}
