using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.Interfaces;
using RentACar.DTO.Admin;
using RentACar.Web.ViewModels.Admin;

namespace RentACar.Web.Areas.Admin.Controllers
{
    public class AdminController : BaseAdminController
    {
        private readonly IAdminService adminService;
        private readonly IMapper mapperService;
        public AdminController(IAdminService _adminService,
            IMapper _mapperService)
        {
           adminService = _adminService;
           mapperService = _mapperService;
        }

        public async Task<IActionResult> Dashboard()
        {
            DashboardDTO dto = await adminService.GetAppInfo();

            DashboardViewModel model = mapperService.Map<DashboardViewModel>(dto);

            return View(model);
        }
    }
}
