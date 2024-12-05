using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.Interfaces;
using RentACar.DTO.Admin;
using RentACar.DTO.Car;
using RentACar.DTO.Role;
using RentACar.DTO.User;
using RentACar.Web.ViewModels.Admin;
using RentACar.Web.ViewModels.Car;
using RentACar.Web.ViewModels.Role;
using RentACar.Web.ViewModels.User;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
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

            if (await carService.FindCarByRegistrationNumberAsync(model.RegistrationNumber))
            {
                ModelState.AddModelError(string.Empty, CarWithThatRegistrationNumberExists);
                return View(model);
            }

            if (model.CarImage == null)
            {
                model.CarImageUrl = $"{Url.Content(NoImageUrl)}";
            }
            else
            {
                string filePath =
                    await fileService.SavePhotoToServerAsync(model.CarImage, model.RegistrationNumber);

                if (filePath == null)
                {
                    ModelState.AddModelError(string.Empty, UploadPhotoError);
                }

                model.CarImageUrl = filePath;
            }


            AddCarDTO dto = mapperService.Map<AddCarDTO>(model);

            bool result = await carService.AddCar(dto);

            if (!result)
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
            if (!base.IsValidGuid(model.Id))
            {
                TempData[ErrorMessageString] = "Невалидно Id на колата";
                return RedirectToAction("ManageCars");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!await carService.FindCarByIdAsync(Guid.Parse(model.Id)))
            {
                TempData[ErrorMessageString] = InvalidCarId;
                return RedirectToAction("ManageCars");
            }

            if (model.CarImage != null)
            {
                string newPhotoPath = await fileService.ChangePhotoAsync(model.CarImage, model.CarImageUrl, model.RegistrationNumber);
                model.CarImageUrl = newPhotoPath;
            }

            EditCarDTO dto = mapperService.Map<EditCarDTO>(model);

            bool result = await carService.EditCarAsync(dto);

            if (!result)
            {
                TempData[ErrorMessageString] = ErrorWhenEditCar;
                return View(model);
            }

            TempData[SuccessfullMessageString] = EditCarSuccessfull;

            return RedirectToAction("ManageCars");
        }

        [HttpGet]
        public async Task<IActionResult> ManageUsers()
        {
            ManagerUsersViewModel model =
                mapperService.Map<ManagerUsersViewModel>(await userService.GetAllUsersWithAllRoles());

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SetUserRole([FromBody] SetRoleViewModel model)
        {
            if (!await adminService.IsUserAdmin(User))
            {
                return Unauthorized();
            }

            if (!await adminService.IsModifyingOwnRole(User, model.UserId))
            {
                return BadRequest(new { status = "Не може да променяш свойте роли." });
            }

            SetRoleDTO dto = mapperService.Map<SetRoleDTO>(model);

            bool result = await adminService.SetRoleToUser(mapperService.Map<SetRoleDTO>(model));

            if (result)
            {
                return Ok(new { status = "Successfull" });
            }

            return BadRequest(new { status = "Възникна грешка при задаването на ролята." });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUserRole([FromBody] SetRoleViewModel model)
        {
            if (!await adminService.IsUserAdmin(User))
            {
                return Unauthorized();
            }

            if (!await adminService.IsModifyingOwnRole(User, model.UserId))
            {
                return BadRequest(new { status = "Не може да променяш свойте роли." });
            }

            SetRoleDTO dto = mapperService.Map<SetRoleDTO>(model);

            bool result = await adminService.DeleteRoleFromUser(dto);

            if (result)
            {
                return Ok(new { status = "successfull" });
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUser([FromBody] DeleteUserViewModel model)
        {
            if (!await adminService.IsUserAdmin(User))
            {
                return Unauthorized();
            }

            if (!await adminService.IsModifyingOwnRole(User, model.Id))
            {
                return BadRequest(new { status = "Не може да изтриваш своя акаунт от тук." });
            }

            DeleteUserDTO dto = mapperService.Map<DeleteUserDTO>(model);

            bool result = await adminService.DeleteUser(dto);

            if (result)
            {
                return Ok(new { status = "successfull" });
            }

            return BadRequest();
        }
    }
}
