using Microsoft.AspNetCore.Mvc;
using RentACar.DTO.Car;
using RentACar.DTO.Result;
using RentACar.Web.ViewModels.Admin;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
using static RentACar.Common.Messages.DatabaseModelsMessages.Car;
using static RentACar.Common.Messages.DatabaseModelsMessages.Common;
using AutoMapper;
using RentACar.Core.Interfaces;
namespace RentACar.Web.Areas.Admin.Controllers
{
    public class CarController : BaseAdminController
    {
        private readonly IMapper mapperService;
        private readonly ICarService carService;
        private readonly IUserService userService;
        public CarController(
            IMapper _mapperService,
            ICarService _carService)
        {
            mapperService = _mapperService;
            carService = _carService;
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
            return RedirectToAction("ManageCars", "Admin");
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

            return RedirectToAction("ManageCars", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> EditCar(string id)
        {
            if (!base.IsValidGuid(id))
            {
                TempData[ErrorMessageString] = InvalidGuidId;
                return RedirectToAction("ManageCars" , "Admin");
            }

            EditCarDTO dto = await carService.CreateEditCarDto(Guid.Parse(id));

            if (dto == null)
            {
                TempData[ErrorMessageString] = InvalidCarId;
                return RedirectToAction("ManageCars" , "Admin");
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

            return RedirectToAction("ManageCars" , "Admin");
        }
    }
}
