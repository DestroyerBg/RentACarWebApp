using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.Interfaces;
using RentACar.DTO.CustomerFeedback;
using RentACar.DTO.Result;
using RentACar.Web.ViewModels.CustomerFeedback;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;
using static RentACar.Common.Messages.DatabaseModelsMessages.CustomerFeedback;
namespace RentACar.Web.Controllers
{
    public class CustomerFeedbackController : BaseController
    {
        private readonly ICustomerFeedbackService customerFeedbackService;
        private readonly IMapper mapperService;
        public CustomerFeedbackController(ICustomerFeedbackService _customerFeedbackService,
            IMapper _mapperService)
        {
            customerFeedbackService = _customerFeedbackService;
            mapperService = _mapperService;
        }
        [HttpGet]
        [Authorize(StardardUserRoleName)]
        public async Task<IActionResult> SendFeedback()
        {
            SendFeedbackViewModel model =
                mapperService.Map<SendFeedbackViewModel>(await customerFeedbackService.CreateSendFeedbackDTO());
            return View(model);
        }

        [HttpPost]
        [Authorize(StardardUserRoleName)]
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

            return RedirectToAction("Index", "Home");
        }
        
    }
}
