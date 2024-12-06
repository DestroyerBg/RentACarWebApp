using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.Interfaces;
using RentACar.DTO.Admin;
using RentACar.DTO.Car;
using RentACar.DTO.Result;
using RentACar.DTO.Role;
using RentACar.DTO.User;
using RentACar.Web.ViewModels.Admin;
using RentACar.Web.ViewModels.Car;
using RentACar.Web.ViewModels.Role;
using RentACar.Web.ViewModels.User;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;
namespace RentACar.Web.Areas.Admin.Controllers
{
    public class AdminController : BaseAdminController
    {
        private readonly IAdminService adminService;
        private readonly IMapper mapperService;
        private readonly IUserService userService;
        public AdminController(IAdminService _adminService,
            IMapper _mapperService,
            IFileService _fileService,
            IUserService _userService)
        {
            adminService = _adminService;
            mapperService = _mapperService;
            userService = _userService;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            DashboardDTO dto = await adminService.GetAppInfo();

            DashboardViewModel model = mapperService.Map<DashboardViewModel>(dto);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ManageCars()
        {
            IEnumerable<CarInformationDTO> dtos = await adminService.GetCarsInformation();
            IEnumerable<CarInformationViewModel> models =
                dtos.Select(d => mapperService.Map<CarInformationViewModel>(d));

            return View(models);
        }

        [HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> ManageUsers()
        {
            ManagerUsersViewModel model =
                mapperService.Map<ManagerUsersViewModel>(await userService.GetAllUsersWithAllRoles());

            return View(model);
        }
    }
}
