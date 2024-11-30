using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.Interfaces;
using RentACar.Data.Models;
using RentACar.DTO.Reservation;
using RentACar.Web.ViewModels.Reservation;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;
namespace RentACar.Web.Controllers
{
    [Authorize(Roles = StardardUserRoleName)]
    public class ReservationController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IReservationService reservationService;
        private readonly IMapper mapperService;

        public ReservationController(UserManager<ApplicationUser> _userManager,
            IReservationService _reservationService,
            IMapper _mapperService)
        {
            userManager = _userManager;
            reservationService = _reservationService;
            mapperService = _mapperService;
        }

        [HttpGet]
        public async Task<IActionResult> MyReservations()
        {
            ApplicationUser? user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            IEnumerable<ReservationDTO> dtos = await reservationService.GetAllReservationForUser(user);

            IEnumerable<ReservationViewModel> models = dtos.Select(d => mapperService.Map<ReservationViewModel>(d));

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (!base.IsValidGuid(id))
            {
                return RedirectToAction("MyReservations");
            }

            ReservationDetailsDTO? dto = await reservationService.GetReservationDetails(Guid.Parse(id));

            if (dto == null)
            {
                return RedirectToAction("MyReservations");
            }

            ReservationDetailsViewModel model = mapperService.Map<ReservationDetailsViewModel>(dto);

            return View(model);
        }
    }
}
