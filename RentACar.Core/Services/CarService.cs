using System.Drawing.Imaging;
using System.Globalization;
using System.Security.Policy;
using AutoMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentACar.Core.Interfaces;
using RentACar.Data.Models;
using RentACar.Data.Repository.Interfaces;
using RentACar.DTO.Car;
using RentACar.DTO.Category;
using RentACar.DTO.Feature;
using RentACar.DTO.InsuranceBenefit;
using RentACar.DTO.Location;
using RentACar.DTO.Reservation;
using RentACar.DTO.Result;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
using static RentACar.Common.Messages.DatabaseModelsMessages.Car;
using static RentACar.Common.Messages.DatabaseModelsMessages.Common;
namespace RentACar.Core.Services
{
    public class CarService : BaseService, ICarService
    {
        private readonly IRepository<Car, Guid> carRepository;
        private readonly IRepository<InsuranceBenefit, Guid> insuranceBenefitRepository;
        private readonly IRepository<Location, Guid> locationRepository;
        private readonly IRepository<Category, Guid> categoryRepository;
        private readonly IRepository<Feature, Guid> featureRepository;
        private readonly IMapper mapperService;
        private readonly IFileService fileService;

        public CarService(IRepository<Car, Guid> _carRepository,
            IMapper _mapperService,
            IRepository<InsuranceBenefit, Guid> _insuranceBenefitRepository,
            IRepository<Location, Guid> _locationRepository,
            IRepository<Category, Guid> _categoryRepository,
            IRepository<Feature, Guid> _featureRepository,
            IFileService _fileService)
        {
            carRepository = _carRepository;
            mapperService = _mapperService;
            insuranceBenefitRepository = _insuranceBenefitRepository;
            locationRepository = _locationRepository;
            categoryRepository = _categoryRepository;
            featureRepository = _featureRepository;
            fileService = _fileService;
        }

        public async Task<IEnumerable<ViewCarDTO>> GetCarsAsync()
        {
            IEnumerable<ViewCarDTO> cars = await carRepository
                .GetAllAttached()
                .Include(c => c.CarFeatures)
                .ThenInclude(c => c.Feature)
                .Select(c => mapperService.Map<Car, ViewCarDTO>(c))
                .ToListAsync();


            return cars;

        }

        public async Task<IEnumerable<ViewCarDTO>> GetCarsFilteredByPriceAsync(string price)
        {
            bool isPriceValid = decimal.TryParse(price, out decimal result);

            if (!isPriceValid)
            {
                return null;
            }

            IEnumerable<ViewCarDTO> dtos = await carRepository
                .GetAllAttached()
                .Include(c => c.CarFeatures)
                .ThenInclude(c => c.Feature)
                .Where(c => c.PricePerDay <= result)
                .Select(c => mapperService.Map<Car, ViewCarDTO>(c))
                .ToListAsync();

            return dtos;
        }

        public async Task<RentACarDTO> ReserveACar(Guid carId)
        {
            Car? car = await carRepository
                .GetAllAttached()
                .Include(c => c.Location)
                .FirstOrDefaultAsync(c => c.Id == carId);

            if (car == null)
            {
                return null;
            }

            ICollection<InsuranceBenefitDTO> insuranceBenefits = await
                insuranceBenefitRepository
                    .GetAllAttached()
                    .Select(i => mapperService.Map<InsuranceBenefit, InsuranceBenefitDTO>(i))
                    .ToListAsync();

            ICollection<LocationDTO> locations = await
                locationRepository
                    .GetAllAttached()
                    .Select(l => mapperService.Map<LocationDTO>(l))
                    .ToListAsync();

            RentACarDTO carDto = mapperService.Map<Car, RentACarDTO>(car);

            carDto.Benefits = insuranceBenefits;
            carDto.Locations = locations;
            return carDto;
        }

        public async Task<ConfirmReservationDTO> CreateReservationConfirmation(CreateReservationDTO reservationDTO)
        {
            if (!base.IsValidGuid(reservationDTO.CarId))
            {
                return null;
            }

            Car? car = await carRepository.GetByIdAsync(Guid.Parse(reservationDTO.CarId));


            if (car == null || !base.IsValidGuid(reservationDTO.LocationId))
            {
                return null;
            }

            Location? location = await locationRepository.GetByIdAsync(Guid.Parse(reservationDTO.LocationId));

            if (location == null)
            {
                return null;
            }

            ConfirmReservationDTO confirmReservation = mapperService.Map<ConfirmReservationDTO>(reservationDTO);

            confirmReservation.TotalPrice = await CalculateTotalPrice(reservationDTO);
            confirmReservation.CarBrand = car.Brand;
            confirmReservation.CarModel = car.Model;
            return confirmReservation;
        }

        public async Task<AddCarDTO> CreateAddCarDto()
        {
            AddCarDTO dto = new AddCarDTO();

            ICollection<CategoryDTO> categories = await categoryRepository
                .GetAllAttached()
                .Select(c => mapperService.Map<CategoryDTO>(c))
                .ToListAsync();

            ICollection<LocationDTO> locations = await locationRepository
                .GetAllAttached()
                .Select(l => mapperService.Map<LocationDTO>(l))
                .ToListAsync();

            ICollection<FeatureCheckboxDTO> features = await featureRepository
                .GetAllAttached()
                .Select(f => mapperService.Map<FeatureCheckboxDTO>(f))
                .ToListAsync();

            dto.Categories = categories;
            dto.Locations = locations;
            dto.Features = features;
            return dto;
        }

        public async Task<ResultWithErrors> AddCar(AddCarDTO dto)
        {
            ResultWithErrors result = new ResultWithErrors();
            if (await FindCarByRegistrationNumberAsync(dto.RegistrationNumber))
            {
                result.Errors.Add(CarWithThatRegistrationNumberExists);
                result.Success = false;

                return result;
            }

            if (dto.CarImage == null)
            {
                dto.CarImageUrl = NoImageUrl;
            }
            else
            {
                string filePath =
                    await fileService.SavePhotoToServerAsync(dto.CarImage, dto.RegistrationNumber);

                if (filePath == null)
                {
                    dto.CarImageUrl = NoImageUrl;
                }

                dto.CarImageUrl = filePath;
            }

            Car car = mapperService.Map<Car>(dto);
            foreach (FeatureCheckboxDTO feature in dto.Features.Where(f => f.IsChecked))
            {
                if (!await FindFeatureByIdAsync(feature.Id))
                {
                    continue;
                }

                CarFeature cf = new CarFeature()
                {
                    FeatureId = Guid.Parse(feature.Id)
                };
                car.CarFeatures.Add(cf);
            }

            try
            {
                await carRepository.AddAsync(car);

                result.Success = true;
                return result;
            }
            catch (Exception e)
            {
                result.Errors.Add(ErrorWhenAddingCar);
                return result;
            }
        }

        public async Task<bool> DeleteCarAsync(Guid id)
        {
            Car? car = await carRepository
                .GetByIdAsync(id);

            if (car == null)
            {
                return false;
            }

            car.IsDeleted = true;

            bool result = await carRepository.SaveChangesAsync();

            return result;
        }

        public async Task<EditCarDTO> CreateEditCarDto(Guid id)
        {
            Car? car = await carRepository
                .GetAllAttached()
                .Include(c => c.CarFeatures)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return null;
            }

            ICollection<CategoryDTO> categories = await categoryRepository
                .GetAllAttached()
                .Select(c => mapperService.Map<CategoryDTO>(c))
                .ToListAsync();

            ICollection<LocationDTO> locations = await locationRepository
                .GetAllAttached()
                .Select(l => mapperService.Map<LocationDTO>(l))
                .ToListAsync();

            ICollection<FeatureCheckboxDTO> features = await featureRepository
                .GetAllAttached()
                .Select(f => mapperService.Map<FeatureCheckboxDTO>(f))
                .ToListAsync();

            EditCarDTO dto = new EditCarDTO()
            {
                Id = car.Id.ToString(),
                Brand = car.Brand,
                Model = car.Model,
                CarImageUrl = car.ImageUrl,
                HorsePower = car.HorsePower,
                PricePerDay = car.PricePerDay,
                YearOfManufacture = car.YearOfManufacture,
                RegistrationNumber = car.RegistrationNumber,
                Categories = categories.Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Selected = car.CategoryId == Guid.Parse(c.Id)
                }).ToList(),
                Locations = locations.Select(l => new SelectListItem()
                {
                    Value = l.Id.ToString(),
                    Text = l.City,
                    Selected = car.LocationId == Guid.Parse(l.Id.ToString()),
                }).ToList(),
                Features = features.Select(f => new SelectListItem()
                {
                    Text = f.Name,
                    Value = f.Id.ToString(),
                    Selected = car.CarFeatures.Any(cf => cf.FeatureId == Guid.Parse(f.Id))
                }).ToList()
            };

            return dto;
        }

        public async Task<ResultWithErrors> EditCarAsync(EditCarDTO dto)
        {
            ResultWithErrors result = new ResultWithErrors();
            if (!base.IsValidGuid(dto.Id))
            {
                result.Errors.Add(InvalidGuidId);
                return result;
            }

            Car? car = await carRepository
                .GetAllAttached()
                .Include(cf => cf.CarFeatures)
                .FirstOrDefaultAsync(c => c.Id == Guid.Parse(dto.Id));

            if (car == null)
            {
                result.Errors.Add(InvalidCarId);
                return result;
            }

            if (dto.CarImage != null)
            {
                string newPhotoPath =
                    await fileService.ChangePhotoAsync(dto.CarImage, dto.CarImageUrl, dto.RegistrationNumber);
                dto.CarImageUrl = newPhotoPath;
            }

            mapperService.Map<EditCarDTO, Car>(dto, car);

            car.CarFeatures.Clear();

            foreach (SelectListItem selectListItem in dto.Features.Where(f => f.Selected))
            {
                if (!await FindFeatureByIdAsync(selectListItem.Value))
                {
                    continue;
                }

                CarFeature cf = new CarFeature()
                {
                    CarId = car.Id,
                    FeatureId = Guid.Parse(selectListItem.Value)
                };

                car.CarFeatures.Add(cf);
            }

            await carRepository.ApplyAsModified(car);
            result.Success = await carRepository.SaveChangesAsync();
            return result;
        }

        public async Task<Result> SetCarAsHired(string id)
        {
            if (!base.IsValidGuid(id))
            {
                return new Result()
                {
                    Message = InvalidGuidId,
                    Success = false
                };
            }

            Car? car = await carRepository.GetByIdAsync(Guid.Parse(id));

            if (car == null)
            {
                return new Result()
                {
                    Message = InvalidCarId,
                    Success = false
                };
            }

            car.IsHired = true;

            try
            {
                await carRepository.SaveChangesAsync();

                return new Result()
                {
                    Message = CarStatusWasChangedToHired,
                    Success = true
                };
            }

            catch (Exception e)
            {
                return new Result()
                {
                    Message = CarStatusChangingError,
                    Success = false
                };
            }
            
        }

        public async Task<Result> ReleaseCar(string id)
        {
            if (!base.IsValidGuid(id))
            {
                return new Result()
                {
                    Message = InvalidGuidId,
                    Success = false
                };
            }

            Car? car = await carRepository.GetByIdAsync(Guid.Parse(id));

            if (car == null)
            {
                return new Result()
                {
                    Message = InvalidCarId,
                    Success = false
                };
            }

            car.IsHired = false;

            try
            {
                await carRepository.SaveChangesAsync();

                return new Result()
                {
                    Message = CarStatusWasChangedТоReleased,
                    Success = true
                };
            }

            catch (Exception e)
            {
                return new Result()
                {
                    Message = CarStatusChangingError,
                    Success = false
                };
            }
        }

        private async Task<decimal> CalculateTotalPrice(CreateReservationDTO reservationDTO)
        {
            Car? car = await carRepository.GetByIdAsync(Guid.Parse(reservationDTO.CarId));
            decimal totalPrice = 0;
            DateTime startDate = DateTime.ParseExact(reservationDTO.StartDate, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);

            DateTime endDate = DateTime.ParseExact(reservationDTO.EndDate, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);

            TimeSpan dateDiff = endDate - startDate;

            if (dateDiff.Days == 1)
            {
                 totalPrice = car.PricePerDay;
            }
            else
            {
                totalPrice = car.PricePerDay * dateDiff.Days;
            }
            

            foreach (InsuranceBenefitDTO benefitDto in reservationDTO.InsuranceBenefits.Where(l => l.IsChecked))
            {
                InsuranceBenefit currBenefit = await insuranceBenefitRepository.GetByIdAsync(benefitDto.Id);

                if (currBenefit == null)
                {
                    continue;
                }

                totalPrice += currBenefit.Price * dateDiff.Days;
            }

            return totalPrice;
        }

        private async Task<bool> FindFeatureByIdAsync(string id)
        {
            if (!IsValidGuid(id))
            {
                return false;
            }

            Feature? feature = await featureRepository.GetByIdAsync(Guid.Parse(id));

            if (feature == null)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> FindCarByRegistrationNumberAsync(string registrationNumber)
        {
            Car? car = await carRepository.FirstOrDefaultAsync(c => c.RegistrationNumber == registrationNumber);

            if (car == null)
            {
                return false;
            }

            return true;
        }
        private async Task<bool> FindCarByIdAsync(Guid id)
        {
            Car? car = await carRepository.GetByIdAsync(id);

            if (car == null)
            {
                return false;
            }

            return true;
        }

    }
}
