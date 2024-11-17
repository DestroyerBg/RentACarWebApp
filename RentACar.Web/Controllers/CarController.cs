using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.Interfaces;
using RentACar.DTO.Car;
using RentACar.Web.ViewModels.Car;

namespace RentACar.Web.Controllers
{
    public class CarController : BaseController
    {
        private readonly ICarService carService;
        private readonly IMapper mapperService;

        public CarController(
            ICarService _carService, 
            IMapper _mapperService)
        {
            carService = _carService;
            mapperService = _mapperService;
        }

        [HttpGet]
        public async Task<IActionResult> AllCars()
        {
            IEnumerable<ViewCarDTO> carDtos = await carService.GetCarsAsync();

            IEnumerable<ViewCarsViewModel> carModels =
                carDtos.Select(car => mapperService.Map<ViewCarDTO, ViewCarsViewModel>(car));

            ViewBag.ShowSideMenu = true;
            return View(carModels);
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> RentACar(string id)
        {
            if (!base.IsValidGuid(id))
            {
                return RedirectToAction(nameof(AllCars));
            }

            RentACarDTO? carDto = await carService.ReserveACar(Guid.Parse(id));

            if (carDto == null)
            {
                return RedirectToAction(nameof(AllCars));
            }

            RentACarViewModel model = mapperService.Map<RentACarDTO, RentACarViewModel>(carDto);

            return View(model);
        }

        
    }
}
