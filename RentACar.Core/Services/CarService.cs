using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentACar.Core.Interfaces;
using RentACar.Data.Models;
using RentACar.Data.Repository;
using RentACar.Data.Repository.Interfaces;
using RentACar.DTO.Car;
using RentACar.DTO.InsuranceBenefit;
using RentACar.DTO.Location;

namespace RentACar.Core.Services
{
    public class CarService : ICarService
    {
        private readonly IRepository<Car, Guid> carRepository;
        private readonly IRepository<InsuranceBenefit, Guid> insuranceBenefitRepository;
        private readonly IRepository<Location, Guid> locationRepository;
        private readonly IMapper mapperService;
        public CarService(IRepository<Car, Guid> _carRepository,
            IMapper _mapperService,
            IRepository<InsuranceBenefit, Guid> _insuranceBenefitRepository,
            IRepository<Location, Guid> _locationRepository)
        {
            carRepository = _carRepository;
            mapperService = _mapperService;
            insuranceBenefitRepository = _insuranceBenefitRepository;
            locationRepository = _locationRepository;
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
    }
}
