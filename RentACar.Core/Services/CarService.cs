using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentACar.Core.Interfaces;
using RentACar.Data.Models;
using RentACar.Data.Repository;
using RentACar.Data.Repository.Interfaces;
using RentACar.DTO.Car;

namespace RentACar.Core.Services
{
    public class CarService : ICarService
    {
        private readonly IRepository<Car, Guid> carRepository;
        private readonly IMapper mapperService;
        public CarService(IRepository<Car, Guid> _carRepository,
            IMapper _mapperService)
        {
            carRepository = _carRepository;
            mapperService = _mapperService;
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
    }
}
