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
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;
using static RentACar.Common.Messages.DatabaseModelsMessages.Car;
using static RentACar.Common.Messages.DatabaseModelsMessages.Common;
namespace RentACar.Web.Areas.Admin.Controllers
{
    public class AdminController : BaseAdminController
    {
        private readonly IAdminService adminService;
        private readonly IMapper mapperService;
        private readonly ICarService carService;
        private readonly IFileService fileService;
        private readonly IUserService userService;
        public AdminController(IAdminService _adminService,
            IMapper _mapperService,
            ICarService _carService,
            IFileService _fileService,
            IUserService _userService)
        {
            adminService = _adminService;
            mapperService = _mapperService;
            carService = _carService;
            fileService = _fileService;
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
        public async Task<IActionResult> AddCar()
        {
            AddCarDTO dto = await carService.CreateAddCarDto();

            AddCarViewModel model = mapperService.Map<AddCarViewModel>(dto);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(AddCarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AddCarDTO dto = mapperService.Map<AddCarDTO>(model);

            ResultWithErrors result = await carService.AddCar(dto);

            if (result.Errors.Any())
            {
                ModelState.AddModelError(string.Empty, ErrorWhenAddingCar);
                return View(model);
            }

            TempData[SuccessfullMessageString] = CarAddedSuccessfully;
            return RedirectToAction("ManageCars");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCar(string id)
        {
            if (!base.IsValidGuid(id))
            {
                TempData[ErrorMessageString] = InvalidGuidId;
            }

            bool result = await carService.DeleteCarAsync(Guid.Parse(id));

            if (!result)
            {
                TempData[ErrorMessageString] = CarDeletionError;
            }
            else
            {
                TempData[SuccessfullMessageString] = CarDeletedSuccessfully;
            }

            return RedirectToAction("ManageCars");
        }

        [HttpGet]
        public async Task<IActionResult> EditCar(string id)
        {
            if (!base.IsValidGuid(id))
            {
                TempData[ErrorMessageString] = InvalidGuidId;
                return RedirectToAction("ManageCars");
            }

            EditCarDTO dto = await carService.CreateEditCarDto(Guid.Parse(id));

            if (dto == null)
            {
                TempData[ErrorMessageString] = InvalidCarId;
                return RedirectToAction("ManageCars");
            }
            EditCarViewModel model = mapperService.Map<EditCarViewModel>(dto);

            return View(model);

            
        }

        [HttpPost]
        public async Task<IActionResult> EditCar(EditCarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            EditCarDTO dto = mapperService.Map<EditCarDTO>(model);

            ResultWithErrors result = await carService.EditCarAsync(dto);

            if (result.Errors.Any())
            {
                ModelState.AddModelError(string.Empty, ErrorWhenAddingCar);
                return View(model);
            }

            TempData[SuccessfullMessageString] = EditCarSuccessfull;

            return RedirectToAction("ManageCars");
        }

        [HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> ManageUsers()
        {
            ManagerUsersViewModel model =
                mapperService.Map<ManagerUsersViewModel>(await userService.GetAllUsersWithAllRoles());

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SetUserRole([FromBody] SetRoleViewModel model)
        {
            SetRoleDTO dto = mapperService.Map<SetRoleDTO>(model);
            Result result = await adminService.SetRoleToUser(mapperService.Map<SetRoleDTO>(model), User);

            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(new { status = result.Message });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUserRole([FromBody] SetRoleViewModel model)
        {
            SetRoleDTO dto = mapperService.Map<SetRoleDTO>(model);

            Result result = await adminService.DeleteRoleFromUser(dto,User);

            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(new{status = result.Message});
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUser([FromBody] DeleteUserViewModel model)
        {
            
            DeleteUserDTO dto = mapperService.Map<DeleteUserDTO>(model);

            Result result = await adminService.DeleteUser(dto, User);

            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(new {status = result.Message});
        }
    }
}
