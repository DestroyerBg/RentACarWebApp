using Microsoft.AspNetCore.Mvc;
using RentACar.Core.Interfaces;
using RentACar.DTO.Car;
using RentACar.Web.ViewModels.Filters;

namespace RentACar.Web.API.Controllers
{
    public class CarController : BaseApiController
    {
        private readonly ICarService carService;

        public CarController(ICarService _carService)
        {
            carService = _carService;
        }
        [HttpPost("FilterCarsByPrice")]
        public async Task<IActionResult> FilterCarsByPrice([FromBody] FilterPrice? price)
        {
            IEnumerable<ViewCarDTO>? dtos = await carService.GetCarsFilteredByPriceAsync(price.Price);

            if (dtos == null)
            {
                return BadRequest();
            }

            if (dtos.Count() ==  0)
            {
                return NotFound();
            }

            return Ok(dtos);
        }
    }
}
