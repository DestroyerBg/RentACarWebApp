using System.Globalization;
using AutoMapper;
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
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
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
        public CarService(IRepository<Car, Guid> _carRepository,
            IMapper _mapperService,
            IRepository<InsuranceBenefit, Guid> _insuranceBenefitRepository,
            IRepository<Location, Guid> _locationRepository,
            IRepository<Category, Guid> _categoryRepository,
            IRepository<Feature, Guid> _featureRepository)
        {
            carRepository = _carRepository;
            mapperService = _mapperService;
            insuranceBenefitRepository = _insuranceBenefitRepository;
            locationRepository = _locationRepository;
            categoryRepository = _categoryRepository;
            featureRepository = _featureRepository;
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
    }
}
