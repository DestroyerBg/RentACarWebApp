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
using RentACar.DTO.Result;
using RentACar.DTO.Role;
using RentACar.DTO.User;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;
using static RentACar.Common.Messages.DatabaseModelsMessages.Common;
using static RentACar.Common.Messages.DatabaseModelsMessages.ApplicationUser;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
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
            RoleManager<IdentityRole<Guid>> _roleManager,
            RentACarDbContext _context,
            IRepository<Car, Guid> _carRepository,
            IRepository<Reservation, Guid> _reservationRepository,
            IMapper _mapperService)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            context = _context;
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

        public async Task<Result> SetRoleToUser(SetRoleDTO dto, ClaimsPrincipal claim)
        {
            if (!IsValidGuid(dto.UserId))
            {
                return new Result()
                {
                    Success = false,
                    Message = InvalidGuidId,
                };
            }

            ApplicationUser? user = await userManager.FindByIdAsync(dto.UserId);

            if (user == null)
            {
                return new Result()
                {
                    Success = false,
                    Message = InvalidUserId
                };
            }

            IdentityRole<Guid>? role = await roleManager.FindByNameAsync(dto.RoleName);

            if (role == null)
            {
                return new Result()
                {
                    Success = false,
                    Message = InvalidRoleId
                };
            }

            if (!await IsModifyingOwnRole(claim, dto.UserId))
            {
                return new Result()
                {
                    Success = false,
                    Message = CannotModifyYourselfARoleRestrictionMessage
                };
            }

            IList<Claim> claims = await GetUserClaims(user);
            if (claims.Any(c => c.Type == SuperAdminClaimType && c.Value == "true"))
            {
                return new Result()
                {
                    Success = false,
                    Message = CannotModifySuperAdmin
                };
            }

            if (dto.RoleName == AdminRoleName)
            {
                bool checkCurrentAdmin = await CheckIfCurrentAdminIsSuperAdmin(claim);

                if (!checkCurrentAdmin)
                {
                    return new Result()
                    {
                        Success = false,
                        Message = CannotSetOtherUsersAdminRole
                    };
                }
            }

            IdentityResult result = await userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return new Result()
                {
                    Success = true,
                    Message = SuccessfullMessageString
                };
            }

            return new Result()
            {
                Success = false,
                Message = ErrorWhenAddingRoles
            };
        }

        public async Task<Result> DeleteRoleFromUser(SetRoleDTO dto, ClaimsPrincipal claim)
        {
            if (!IsValidGuid(dto.UserId))
            {
                return new Result()
                {
                    Success = false,
                    Message = InvalidGuidId,
                };
            }

            if (!await IsModifyingOwnRole(claim, dto.UserId))
            {
                return new Result()
                {
                    Success = false,
                    Message = CannotModifyYourselfARoleRestrictionMessage
                };
            }

            ApplicationUser? user = await userManager.FindByIdAsync(dto.UserId);

            if (user == null)
            {
                return new Result()
                {
                    Success = false,
                    Message = InvalidUserId
                };
            }

            IList<Claim> claims = await GetUserClaims(user);
            if (claims.Any(c => c.Type == SuperAdminClaimType && c.Value == "true"))
            {
                return new Result()
                {
                    Success = false,
                    Message = CannotModifySuperAdmin
                };
            }

            IdentityRole<Guid>? role = await roleManager.FindByNameAsync(dto.RoleName);

            if (role == null)
            {
                return new Result()
                {
                    Success = false,
                    Message = InvalidRoleId
                };
            }

            IdentityResult result = await userManager.RemoveFromRoleAsync(user, dto.RoleName);

            if (result.Succeeded)
            {
                return new Result()
                {
                    Success = true,
                    Message = SuccessfullMessageString
                };
            }

            return new Result()
            {
                Success = false,
                Message = ErrorWhenDeletingRoles
            };

        }

        public async Task<Result> DeleteUser(DeleteUserDTO dto, ClaimsPrincipal claim)
        {
            if (!IsValidGuid(dto.Id))
            {
                return new Result()
                {
                    Success = false,
                    Message = InvalidGuidId,
                };
            }

            if (!await IsModifyingOwnRole(claim, dto.Id))
            {
                return new Result()
                {
                    Success = false,
                    Message = CannotModifyYourselfARoleRestrictionMessage
                };
            }

            ApplicationUser? user = await userManager.FindByIdAsync(dto.Id);

            if (user == null)
            {
                return new Result()
                {
                    Success = false,
                    Message = InvalidUserId
                };
            }

            IList<Claim> claims = await GetUserClaims(user);
            if (claims.Any(c => c.Type == SuperAdminClaimType && c.Value == "true"))
            {
                return new Result()
                {
                    Success = false,
                    Message = CannotModifySuperAdmin
                };
            }

            user.IsDeleted = true;
            try
            {
                await context.SaveChangesAsync();
                return new Result()
                {
                    Success = true,
                    Message = SuccessfullMessageString
                };
            }
            catch (Exception e)
            {
                return new Result()
                {
                    Success = false,
                    Message = ErrorWhenDeletingUser
                };
            }

        }

        private async Task<bool> IsModifyingOwnRole(ClaimsPrincipal claim, string targetUserId)
        {
            ApplicationUser? admin = await userManager.GetUserAsync(claim);

            ApplicationUser? targetUser = await userManager.FindByIdAsync(targetUserId);

            if (admin?.Id == targetUser?.Id)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> CheckIfCurrentAdminIsSuperAdmin(ClaimsPrincipal claim)
        {
            ApplicationUser? user = await userManager.GetUserAsync(claim);
            IList<Claim> userClaims = await userManager.GetClaimsAsync(user);

            return userClaims.Any(c => c.Type == SuperAdminClaimType && c.Value == SuperAdminClaimValue);
        }

        private async Task<IList<Claim>> GetUserClaims(ApplicationUser user)
        {
            IList<Claim> claims = await userManager.GetClaimsAsync(user);

            return claims;
        }
    }
}
