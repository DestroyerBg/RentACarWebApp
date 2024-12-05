using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentACar.Core.Interfaces;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.Data.Repository.Interfaces;
using RentACar.DTO.Admin;
using RentACar.DTO.Car;
using RentACar.DTO.Role;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;
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
            IMapper _mapperService,
            RoleManager<IdentityRole<Guid>> _roleManager)
        {
            userManager = _userManager;
            carRepository = _carRepository;
            reservationRepository = _reservationRepository;
            mapperService = _mapperService;
            roleManager = _roleManager;
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

        public async Task<bool> IsUserAdmin(ClaimsPrincipal claim)
        {
            ApplicationUser? user = await userManager.GetUserAsync(claim);

            if (user == null)
            {
                return false;
            }

            if (!await userManager.IsInRoleAsync(user, AdminRoleName))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> SetRoleToUser(SetRoleDTO dto)
        {
            if (!IsValidGuid(dto.UserId))
            {
                return false;
            }

            ApplicationUser? user = await userManager.FindByIdAsync(dto.UserId);

            if (user == null)
            {
                return false;
            }

            IdentityRole<Guid>? role = await roleManager.FindByNameAsync(dto.RoleName);

            if (role == null)
            {
                return false;
            }

            IdentityResult result = await userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteRoleFromUser(SetRoleDTO dto)
        {
            if (!IsValidGuid(dto.UserId))
            {
                return false;
            }

            ApplicationUser? user = await userManager.FindByIdAsync(dto.UserId);

            if (user == null)
            {
                return false;
            }

            IdentityRole<Guid>? role = await roleManager.FindByNameAsync(dto.RoleName);

            if (role == null)
            {
                return false;
            }

            IdentityResult result = await userManager.RemoveFromRoleAsync(user, dto.RoleName);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }
    }
}
