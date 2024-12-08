using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.Interfaces;
using RentACar.DTO.CustomerFeedback;
using RentACar.DTO.Result;
using RentACar.Web.ViewModels.CustomerFeedback;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;
using static RentACar.Common.Constants.DatabaseModelsConstants.CustomerFeedback;
using static RentACar.Common.Messages.DatabaseModelsMessages.CustomerFeedback;
namespace RentACar.Web.Controllers
{
    public class CustomerFeedbackController : BaseController
    {
        private readonly ICustomerFeedbackService customerFeedbackService;
        private readonly IAdminService adminService;
        private readonly IMapper mapperService;
        public CustomerFeedbackController(ICustomerFeedbackService _customerFeedbackService,
            IAdminService _adminService,
            IMapper _mapperService)
        {
            customerFeedbackService = _customerFeedbackService;
            adminService = _adminService;
            mapperService = _mapperService;
        }
        [HttpGet]
        [Authorize(Roles = $"{StardardUserRoleName},{ModeratorRoleName},{AdminRoleName}")]
        public async Task<IActionResult> SendFeedback()
        {
            SendFeedbackViewModel model =
                mapperService.Map<SendFeedbackViewModel>(await customerFeedbackService.CreateSendFeedbackDTO());
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = $"{StardardUserRoleName},{ModeratorRoleName},{AdminRoleName}")]
        public async Task<IActionResult> SendFeedback(SendFeedbackViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            SendFeedbackDTO dto = mapperService.Map<SendFeedbackDTO>(model);

            Result result =
                await customerFeedbackService.CreateFeedback(dto, User);

            if (result.Success)
            {
                TempData[SuccessfullMessageString] = SuccessfullAddedFeedback;
            }
            else
            {
                TempData[ErrorMessageString] = result.Message;
                return View(model);
            }

            return RedirectToAction("AllFeedbacks");
        }

        [HttpGet]
        public async Task<IActionResult> AllFeedbacks()
        {
            IEnumerable<UserFeedbackDTO> dtos = await customerFeedbackService.GetAllFeedbacks();

            IEnumerable<UserFeedbackViewModel> models = dtos.Select(d => mapperService.Map<UserFeedbackViewModel>(d));

            if (await adminService.IsUserAdmin(User) || await adminService.IsUserModerator(User))
            {
                TempData[ShowDeleteOptionString] = true;
            }

            return View(models);
        }

        [HttpPost]
        [Authorize(Roles = $"{ModeratorRoleName},{AdminRoleName}")]
        public async Task<IActionResult> DeleteFeedback([FromQuery]string id)
        {
            Result result = await customerFeedbackService.DeleteFeedback(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
