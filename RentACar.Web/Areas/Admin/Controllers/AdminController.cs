using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.Interfaces;
using RentACar.DTO.Admin;
using RentACar.DTO.Car;
using RentACar.Web.ViewModels.Admin;
using RentACar.Web.ViewModels.Car;

namespace RentACar.Web.Areas.Admin.Controllers
{
    public class AdminController : BaseAdminController
    {
        private readonly IAdminService adminService;
        private readonly IMapper mapperService;
        private readonly ICarService carService;
        private readonly IFileService fileService;
        public AdminController(IAdminService _adminService,
            IMapper _mapperService,
            ICarService _carService,
            IFileService _fileService)
        {
            adminService = _adminService;
            mapperService = _mapperService;
            carService = _carService;
            fileService = _fileService;
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
                ModelState.AddModelError(string.Empty, "Вече е добавена кола с този регистрационен номер.");
                return View(model);
            }

            if (model.CarImage == null)
            {
                model.CarImageUrl = $"{Url.Content("~/images/cars/no-image.jpg")}";
            }
            else
            {
                string filePath =
                    await fileService.SavePhotoToServerAsync(model.CarImage);

                if (filePath == null)
                {
                    ModelState.AddModelError(string.Empty,"Снимката не можа да се качи успешно. Опитай пак!");
                }

                model.CarImageUrl = filePath;
            }


            AddCarDTO dto = mapperService.Map<AddCarDTO>(model);

            bool result = await carService.AddCar(dto);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Възникна грешка при добавяне на колата.");
                return View(model);
            }

            TempData["Successfull"] = "Колата е добавена успешно!";
            return RedirectToAction("ManageCars");
        }
    }
}
