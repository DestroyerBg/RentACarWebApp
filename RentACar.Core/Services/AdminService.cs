using Microsoft.AspNetCore.Identity;
using RentACar.Core.Interfaces;
using RentACar.Data.Models;
using RentACar.Data.Repository;
using RentACar.Data.Repository.Interfaces;
using RentACar.DTO.Admin;

namespace RentACar.Core.Services
{
    public class AdminService : BaseService, IAdminService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository<Car, Guid> carRepository;
        private readonly IRepository<Reservation, Guid> reservationRepository;
        public AdminService(UserManager<ApplicationUser> _userManager,
            IRepository<Car, Guid> _carRepository,
            IRepository<Reservation, Guid> _reservationRepository)
        {
            userManager = _userManager;
            carRepository = _carRepository;
            reservationRepository = _reservationRepository;
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
    }
}
