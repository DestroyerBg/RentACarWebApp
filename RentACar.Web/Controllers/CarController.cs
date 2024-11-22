using System.Security.Claims;
using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.Interfaces;
using RentACar.Core.Services;
using RentACar.DTO.Car;
using RentACar.DTO.Reservation;
using RentACar.Web.ViewModels.Car;
using RentACar.Web.ViewModels.Reservation;

namespace RentACar.Web.Controllers
{
    public class CarController : BaseController
    {
        private readonly ICarService carService;
        private readonly IReservationService reservationService; 
        private readonly IMapper mapperService;

        public CarController(
            ICarService _carService, 
            IMapper _mapperService,
            IReservationService _reservationService)
        {
            carService = _carService;
            mapperService = _mapperService;
            reservationService = _reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> AllCars()
        {
            IEnumerable<ViewCarDTO> carDtos = await carService.GetCarsAsync();

            IEnumerable<ViewCarsViewModel> carModels =
                carDtos.Select(car => mapperService.Map<ViewCarsViewModel>(car));

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

            RentACarViewModel model = mapperService.Map<RentACarViewModel>(carDto);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> RentACar(RentACarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            CreateReservationDTO reservationDto = mapperService.Map<CreateReservationDTO>(model);
            ConfirmReservationDTO dto = await carService.CreateReservationConfirmation(reservationDto);

            if (dto == null)
            {
                return View(model);
            }

            string? userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            dto.CustomerId = userId;

            HttpContext.Session.SetString("Reservation", JsonSerializer.Serialize(dto));
            ConfirmReservationViewModel reservationModel = mapperService.Map<ConfirmReservationViewModel>(dto);

            return View("ConfirmReservation", reservationModel);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmReservation(ConfirmReservationViewModel model)
        {
            string? sessionData = HttpContext.Session.GetString("Reservation");

            if (sessionData == null)
            {
                return RedirectToAction(nameof(AllCars));
            }
            ConfirmReservationDTO? dto = JsonSerializer.Deserialize<ConfirmReservationDTO>(sessionData);    

            bool isCompleted = await reservationService.CreateReservation(dto);

            ReservationDoneViewModel doneModel = mapperService.Map<ReservationDoneViewModel>(dto);

            return View("ReservationDone", doneModel);
        }

        
    }
}
