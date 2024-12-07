using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.Interfaces;
using RentACar.Web.ViewModels.CustomerFeedback;

namespace RentACar.Web.Controllers
{
    public class CustomerFeedbackController : Controller
    {
        private readonly ICustomerFeedbackService customerFeedbackService;
        private readonly IMapper mapperService;
        public CustomerFeedbackController(ICustomerFeedbackService _customerFeedbackService,
            IMapper _mapperService)
        {
            customerFeedbackService = _customerFeedbackService;
            mapperService = _mapperService;
        }
        public async Task<IActionResult> SendFeedback()
        {
            SendFeedbackViewModel model =
                mapperService.Map<SendFeedbackViewModel>(await customerFeedbackService.CreateSendFeedbackDTO());
            return View(model);
        }
    }
}
