using Microsoft.AspNetCore.Mvc;
using RentACar.Core.Interfaces;
using RentACar.DTO.Reservation;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
using static RentACar.Common.Messages.DatabaseModelsMessages.Common;
using static RentACar.Common.Messages.DatabaseModelsMessages.Reservation;
namespace RentACar.Web.Areas.Admin.Controllers
{
    public class ReservationController : BaseAdminController
    {
        private readonly IReservationService reservationService;

        public ReservationController(IReservationService _reservationService)
        {
            reservationService = _reservationService;
        }
        public async Task<IActionResult> GetReservationDetails([FromQuery] string id)
        {
            if (!base.IsValidGuid(id))
            {
                TempData[ErrorMessageString] = InvalidGuidId;
            }
            ReservationDetailsDTO dto = await reservationService.GetReservationDetails(Guid.Parse(id));

            if (dto == null)
            {
                TempData[ErrorMessageString] = ReservationWithThatIdIsNotAvailable;
                return BadRequest();
            }

            return Ok(Json(dto));
        }
    }
}
