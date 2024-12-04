using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentACar.Core.Interfaces;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.Data.Repository.Interfaces;
using RentACar.DTO.Admin;
using RentACar.DTO.Car;
using RentACar.DTO.User;

namespace RentACar.Core.Services
{
    public class AdminService : BaseService, IAdminService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly RentACarDbContext context;
        private readonly IRepository<Car, Guid> carRepository;
        private readonly IRepository<Reservation, Guid> reservationRepository;
        private readonly IMapper mapperService;
        public AdminService(UserManager<ApplicationUser> _userManager,
            IRepository<Car, Guid> _carRepository,
            IRepository<Reservation, Guid> _reservationRepository,
            IMapper _mapperService)
        {
            userManager = _userManager;
            carRepository = _carRepository;
            reservationRepository = _reservationRepository;
            mapperService = _mapperService;
        }
        public async Task<DashboardDTO> GetAppInfo()
        {
            DashboardDTO dashboard = new DashboardDTO()
            {
                TotalCars = carRepository.GetAllAttached().Count(),
                TotalReservations = reservationRepository.GetAllAttached().Count(),
                TotalUsers = userManager.Users.Count()
            };

            return dashboard;
        }

        public async Task<IEnumerable<CarInformationDTO>> GetCarsInformation()
        {
            IEnumerable<CarInformationDTO> cars = await carRepository
                .GetAllAttached()
                .Include(c => c.Location)
                .Select(c => mapperService.Map<CarInformationDTO>(c))
                .ToListAsync();

            return cars;
        }
    }
}
