using System.Globalization;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RentACar.Common.Constants;
using RentACar.Data.Helpers;
using RentACar.Data.Models;
using RentACar.DTO.Identity;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;
namespace RentACar.Data.Seeder
{
    public static class IdentitySeeder
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            string jsonContent = JsonReader.ReadJson("Roles.json");
            RoleManager<IdentityRole<Guid>> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            List<IdentityRole<Guid>>? roles = JsonSerializer.Deserialize<List<IdentityRole<Guid>>>(jsonContent);

            foreach (IdentityRole<Guid> role in roles)
            {
                role.ConcurrencyStamp = Guid.NewGuid().ToString();
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }

            await SeedAdminProfileAndApplyAllRoles(serviceProvider);
            await SeedUserProfileAndApplyUserRole(serviceProvider);
            await SeedModeratorProfileAndApplyModeratorRole(serviceProvider);
        }

        private static async Task SeedAdminProfileAndApplyAllRoles(IServiceProvider serviceProvider)
        {
            string jsonContent = JsonReader.ReadJson("Admin.json");
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            IUserStore<ApplicationUser> userStore = serviceProvider.GetRequiredService<IUserStore<ApplicationUser>>();
            RegisterDTO? dto = JsonSerializer.Deserialize<RegisterDTO>(jsonContent);
            ApplicationUser isUserAlreadyCreated = await userManager.FindByEmailAsync(dto.Email);

            if (isUserAlreadyCreated != null)
            {
                return;
            }

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = DateTime.ParseExact(dto.BirthDate,DatabaseModelsConstants.Common.DateFormat,CultureInfo.InvariantCulture, DateTimeStyles.None),
                PhoneNumber = dto.PhoneNumber
            };

            await userStore.SetUserNameAsync(user, dto.Username, CancellationToken.None);
            await userManager.SetEmailAsync(user, dto.Email);
            await userManager.CreateAsync(user, dto.Password);

            RoleManager<IdentityRole<Guid>> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            List<IdentityRole<Guid>> roles = await roleManager.Roles.ToListAsync();

            foreach (IdentityRole<Guid> role in roles)
            {
                if (!await userManager.IsInRoleAsync(user, role.Name))
                {
                    await userManager.AddToRoleAsync(user, role.Name);
                }
            }

            IList<Claim> existingClaims = await userManager.GetClaimsAsync(user);

            if (!existingClaims.Any(c => c.Type == SuperAdminClaimType && c.Value == SuperAdminClaimValue))
            {
                await userManager.AddClaimAsync(user, new Claim(SuperAdminClaimType, SuperAdminClaimValue));
            }


        }

        private static async Task SeedUserProfileAndApplyUserRole(IServiceProvider serviceProvider)
        {
            string jsonContent = JsonReader.ReadJson("User.json");
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            IUserStore<ApplicationUser> userStore = serviceProvider.GetRequiredService<IUserStore<ApplicationUser>>();
            RegisterDTO? dto = JsonSerializer.Deserialize<RegisterDTO>(jsonContent);
            ApplicationUser isUserAlreadyCreated = await userManager.FindByEmailAsync(dto.Email);

            if (isUserAlreadyCreated != null)
            {
                return;
            }

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = DateTime.ParseExact(dto.BirthDate, DatabaseModelsConstants.Common.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None),
                PhoneNumber = dto.PhoneNumber
            };

            await userStore.SetUserNameAsync(user, dto.Username, CancellationToken.None);
            await userManager.SetEmailAsync(user, dto.Email);
            await userManager.CreateAsync(user, dto.Password);

            RoleManager<IdentityRole<Guid>> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            if (!await userManager.IsInRoleAsync(user, "User"))
            {
                await userManager.AddToRoleAsync(user, "User");
            }
        }

        private static async Task SeedModeratorProfileAndApplyModeratorRole(IServiceProvider serviceProvider)
        {
            string jsonContent = JsonReader.ReadJson("Moderator.json");
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            IUserStore<ApplicationUser> userStore = serviceProvider.GetRequiredService<IUserStore<ApplicationUser>>();
            RegisterDTO? dto = JsonSerializer.Deserialize<RegisterDTO>(jsonContent);
            ApplicationUser isUserAlreadyCreated = await userManager.FindByEmailAsync(dto.Email);

            if (isUserAlreadyCreated != null)
            {
                return;
            }

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = DateTime.ParseExact(dto.BirthDate, DatabaseModelsConstants.Common.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None),
                PhoneNumber = dto.PhoneNumber
            };

            await userStore.SetUserNameAsync(user, dto.Username, CancellationToken.None);
            await userManager.SetEmailAsync(user, dto.Email);
            await userManager.CreateAsync(user, dto.Password);

            RoleManager<IdentityRole<Guid>> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            if (!await userManager.IsInRoleAsync(user, "User"))
            {
                await userManager.AddToRoleAsync(user, "User");
            }

            if (!await userManager.IsInRoleAsync(user, "Moderator"))
            {
                await userManager.AddToRoleAsync(user, "Moderator");
            }
        }
    }
}
