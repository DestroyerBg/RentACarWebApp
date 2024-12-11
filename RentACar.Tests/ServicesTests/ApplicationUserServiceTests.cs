using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using RentACar.Core.Services;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.DTO.Identity;
using RentACar.DTO.User;
using RentACar.Tests.Helpers;
using System.Globalization;
using System.Security.Claims;
using MockQueryable.EntityFrameworkCore;
using RentACar.Tests.Helpers;
using static RentACar.Tests.Helpers.AsyncQueryable;
namespace RentACar.Tests.ServicesTests
{
    public class ApplicationUserServiceTests
    {
        private Mock<SignInManager<ApplicationUser>> mockSignInManager;
        private Mock<UserManager<ApplicationUser>> mockUserManager;
        private Mock<RoleManager<IdentityRole<Guid>>> mockRoleManager;
        private Mock<IUserStore<ApplicationUser>> mockUserStore;
        private Mock<IEmailSender> mockEmailSender;
        private Mock<IMapper> mockMapper;
        private Mock<RentACarDbContext> mockDbContext;
        private ApplicationUserService applicationUserService;

        [SetUp]
        public void Setup()
        {
            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();

            mockSignInManager = new Mock<SignInManager<ApplicationUser>>(
                new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null).Object,
                contextAccessor.Object,
                claimsFactory.Object,
                null, null, null, null);

            mockUserManager = new Mock<UserManager<ApplicationUser>>(
                userStore.Object, null, null, null, null, null, null, null, null);

            mockRoleManager = new Mock<RoleManager<IdentityRole<Guid>>>(
                new Mock<IRoleStore<IdentityRole<Guid>>>().Object, null, null, null, null);

            mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            mockEmailSender = new Mock<IEmailSender>();
            mockMapper = new Mock<IMapper>();
            mockDbContext = new Mock<RentACarDbContext>();

            applicationUserService = new ApplicationUserService(
                mockSignInManager.Object,
                mockUserManager.Object,
                mockUserStore.Object,
                mockEmailSender.Object,
                mockMapper.Object,
                mockRoleManager.Object,
                mockDbContext.Object
            );
        }

        [Test]
        public async Task LogoutUserAsync_ShouldCallSignOutAsync()
        {
            mockSignInManager.Setup(m => m.SignOutAsync()).Returns(Task.CompletedTask);

            var result = await applicationUserService.LogoutUserAsync();

            Assert.IsTrue(result);
            mockSignInManager.Verify(m => m.SignOutAsync(), Times.Once);
        }

        [Test]
        public void CreateBlankRegisterViewModel_ShouldReturnNewRegisterDTO()
        {
            var result = applicationUserService.CreateBlankRegisterViewModel();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RegisterDTO>(result);
        }

        [Test]
        public async Task RegisterUserAsync_ShouldReturnFailedResult_WhenEmailAlreadyExists()
        {
            var dto = new RegisterDTO
            {
                Email = "existingemail@test.com",
                Username = "newuser",
                Password = "Password123!",
                ConfirmPassword = "Password123!"
            };

            var existingUserByEmail = new ApplicationUser { Email = dto.Email };

            mockUserManager.Setup(m => m.FindByEmailAsync(dto.Email)).ReturnsAsync(existingUserByEmail);

            var result = await applicationUserService.RegisterUserAsync(dto);

            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual("DuplicateEmail", result.Errors.First().Code);
            Assert.AreEqual("Вече съществува потребител с този имейл.", result.Errors.First().Description);
            mockUserManager.Verify(m => m.FindByEmailAsync(dto.Email), Times.Once);
            mockUserManager.Verify(m => m.FindByNameAsync(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task RegisterUserAsync_ShouldReturnFailedResult_WhenUsernameAlreadyExists()
        {
            var dto = new RegisterDTO
            {
                Email = "newemail@test.com",
                Username = "existinguser",
                Password = "Password123!",
                ConfirmPassword = "Password123!"
            };

            var existingUserByUsername = new ApplicationUser { UserName = dto.Username };

            mockUserManager.Setup(m => m.FindByEmailAsync(dto.Email)).ReturnsAsync((ApplicationUser)null);
            mockUserManager.Setup(m => m.FindByNameAsync(dto.Username)).ReturnsAsync(existingUserByUsername);

            var result = await applicationUserService.RegisterUserAsync(dto);

            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual("DuplicateUsername", result.Errors.First().Code);
            Assert.AreEqual("Вече съществува потребител с това потребителско име.", result.Errors.First().Description);
            mockUserManager.Verify(m => m.FindByEmailAsync(dto.Email), Times.Once);
            mockUserManager.Verify(m => m.FindByNameAsync(dto.Username), Times.Once);
        }

        [Test]
        public async Task RegisterUserAsync_ShouldReturnSuccessResult_WhenRegistrationIsSuccessful()
        {

            var dto = new RegisterDTO
            {
                Email = "newemail@test.com",
                Username = "newuser",
                Password = "Password123!",
                ConfirmPassword = "Password123!"
            };

            mockUserManager.Setup(m => m.FindByEmailAsync(dto.Email)).ReturnsAsync((ApplicationUser)null);
            mockUserManager.Setup(m => m.FindByNameAsync(dto.Username)).ReturnsAsync((ApplicationUser)null);
            mockMapper.Setup(m => m.Map<RegisterDTO, ApplicationUser>(dto)).Returns(new ApplicationUser { Email = dto.Email, UserName = dto.Username });
            mockUserManager.Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), dto.Password)).ReturnsAsync(IdentityResult.Success);
            mockUserManager.Setup(m => m.AddToRoleAsync(It.IsAny<ApplicationUser>(), "User")).ReturnsAsync(IdentityResult.Success);

            var result = await applicationUserService.RegisterUserAsync(dto);

            Assert.IsTrue(result.Succeeded);
            mockUserManager.Verify(m => m.FindByEmailAsync(dto.Email), Times.Once);
            mockUserManager.Verify(m => m.FindByNameAsync(dto.Username), Times.Once);
            mockUserManager.Verify(m => m.CreateAsync(It.IsAny<ApplicationUser>(), dto.Password), Times.Once);
            mockUserManager.Verify(m => m.AddToRoleAsync(It.IsAny<ApplicationUser>(), "User"), Times.Once);
        }

        [Test]
        public void CreateBlankLoginViewModel_ShouldReturnEmptyLoginDTO()
        {
            var result = applicationUserService.CreateBlankLoginViewModel();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<LoginDTO>(result);
        }

        [Test]
        public async Task LoginUserAsync_ShouldReturnEmptySignInResult_WhenUserNotFound()
        {
            var dto = new LoginDTO
            {
                Email = "nonexistent@email.com",
                Password = "password123",
                RememberMe = false
            };

            mockUserManager.Setup(m => m.FindByEmailAsync(dto.Email))
                .ReturnsAsync((ApplicationUser)null);

            var result = await applicationUserService.LoginUserAsync(dto);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<SignInResult>(result);
            Assert.IsFalse(result.Succeeded);
        }

        [Test]
        public async Task LoginUserAsync_ShouldReturnSucceededSignInResult_WhenLoginIsSuccessful()
        {
            var dto = new LoginDTO
            {
                Email = "test@email.com",
                Password = "password123",
                RememberMe = true
            };

            var user = new ApplicationUser { UserName = "TestUser" };

            mockUserManager.Setup(m => m.FindByEmailAsync(dto.Email))
                .ReturnsAsync(user);

            mockSignInManager.Setup(m => m.PasswordSignInAsync(user.UserName, dto.Password, dto.RememberMe, true))
                .ReturnsAsync(SignInResult.Success);

            var result = await applicationUserService.LoginUserAsync(dto);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Succeeded);
        }

        [Test]
        public async Task LoginUserAsync_ShouldReturnFailedSignInResult_WhenLoginFails()
        {
            var dto = new LoginDTO
            {
                Email = "test@email.com",
                Password = "wrongpassword",
                RememberMe = false
            };

            var user = new ApplicationUser { UserName = "TestUser" };

            mockUserManager.Setup(m => m.FindByEmailAsync(dto.Email))
                .ReturnsAsync(user);

            mockSignInManager.Setup(m => m.PasswordSignInAsync(user.UserName, dto.Password, dto.RememberMe, true))
                .ReturnsAsync(SignInResult.Failed);

            var result = await applicationUserService.LoginUserAsync(dto);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded);
        }

        [Test]
        public void CreateEditProfileDTO_ShouldMapUserToEditProfileDTO()
        {
            // Arrange
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = "TestUser",
                Email = "test@email.com",
                PhoneNumber = "123456789",
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateTime(1990, 1, 1)
            };

            var expectedDto = new EditProfileDTO
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate.ToString("dd.MM.yyyy")
            };

            mockMapper.Setup(m => m.Map<EditProfileDTO>(user)).Returns(expectedDto);

            var result = applicationUserService.CreateEditProfileDTO(user);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedDto.Id, result.Id);
            Assert.AreEqual(expectedDto.Email, result.Email);
            Assert.AreEqual(expectedDto.Username, result.Username);
            Assert.AreEqual(expectedDto.FirstName, result.FirstName);
            Assert.AreEqual(expectedDto.LastName, result.LastName);
            Assert.AreEqual(expectedDto.PhoneNumber, result.PhoneNumber);
            Assert.AreEqual(expectedDto.BirthDate, result.BirthDate);
            mockMapper.Verify(m => m.Map<EditProfileDTO>(user), Times.Once);
        }

        [Test]
        public async Task EditProfile_ShouldReturnTrue_WhenProfileIsUpdatedSuccessfully()
        {
            var dto = new EditProfileDTO
            {
                Id = Guid.NewGuid().ToString(),
                Email = "newemail@example.com",
                Username = "NewUsername",
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateTime(1990, 1, 1).ToString("dd.MM.yyyy"),
                PhoneNumber = "0898395359"
            };

            var user = new ApplicationUser
            {
                Id = Guid.Parse(dto.Id),
                Email = "oldemail@example.com",
                UserName = "OldUsername",
                FirstName = "OldFirstName",
                LastName = "OldLastName",
                BirthDate = new DateTime(1980, 1, 1),
                PhoneNumber = "0898395353"
            };

            mockUserManager.Setup(m => m.FindByIdAsync(dto.Id)).ReturnsAsync(user);
            mockUserManager.Setup(m => m.SetEmailAsync(user, dto.Email)).ReturnsAsync(IdentityResult.Success);
            mockUserManager.Setup(m => m.SetUserNameAsync(user, dto.Username)).ReturnsAsync(IdentityResult.Success);
            mockUserManager.Setup(m => m.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);

            var result = await applicationUserService.EditProfile(dto);

            Assert.IsTrue(result);
            mockUserManager.Verify(m => m.FindByIdAsync(dto.Id), Times.Once);
            mockUserManager.Verify(m => m.SetEmailAsync(user, dto.Email), Times.Once);
            mockUserManager.Verify(m => m.SetUserNameAsync(user, dto.Username), Times.Once);
            mockUserManager.Verify(m => m.UpdateAsync(It.IsAny<ApplicationUser>()), Times.Once);

            Assert.AreEqual(dto.FirstName, user.FirstName);
            Assert.AreEqual(dto.LastName, user.LastName);
            Assert.AreEqual(dto.PhoneNumber, user.PhoneNumber);
            Assert.AreEqual(DateTime.ParseExact(dto.BirthDate, "dd.MM.yyyy", CultureInfo.InvariantCulture), user.BirthDate);
        }

        [Test]
        public async Task EditProfile_ShouldReturnFalse_WhenUserNotFound()
        {
            var dto = new EditProfileDTO
            {
                Id = Guid.NewGuid().ToString(),
                Email = "newemail@example.com",
                Username = "NewUsername",
                FirstName = "John",
                LastName = "Doe",
                BirthDate = "1990-01-01",
                PhoneNumber = "123456789"
            };

            mockUserManager.Setup(m => m.FindByIdAsync(dto.Id)).ReturnsAsync((ApplicationUser)null);

            var result = await applicationUserService.EditProfile(dto);

            Assert.IsFalse(result);

            mockUserManager.Verify(m => m.FindByIdAsync(dto.Id), Times.Once);

        }

        [Test]
        public void GenerateChangePasswordNumberAsync_ShouldReturnSixDigitNumber()
        {
            string result = applicationUserService.GenerateChangePasswordNumberAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(6, result.Length);
            Assert.IsTrue(int.TryParse(result, out int parsedNumber));
            Assert.IsTrue(parsedNumber >= 100000 && parsedNumber <= 999999);
        }

        [Test]
        public void GenerateNewChangePasswordDto_ShouldReturnNewInstanceOfChangePasswordDTO()
        {
            var result = applicationUserService.GenerateNewChangePasswordDto();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ChangePasswordDTO>(result);
        }

        [Test]
        public async Task ChangePasswordWithOldPassword_ShouldReturnCannotFindLoggedInUser_WhenUserIsNull()
        {
            var dto = new ChangePasswordDTO
            {
                OldPassword = "oldPassword123",
                NewPassword = "newPassword123",
                ConfirmPassword = "newPassword123"
            };

            var principal = new ClaimsPrincipal();

            mockUserManager.Setup(m => m.GetUserAsync(principal)).ReturnsAsync((ApplicationUser)null);

            var result = await applicationUserService.ChangePasswordWithOldPassword(dto, principal);

            Assert.AreEqual("Грешка при намирането на текущо логнатия потребител.", result);
            mockUserManager.Verify(m => m.GetUserAsync(principal), Times.Once);
        }

        [Test]
        public async Task ChangePasswordWithOldPassword_ShouldReturnNewPasswordIsDifferentThanOldPassword_WhenPasswordsDoNotMatch()
        {
            var dto = new ChangePasswordDTO
            {
                OldPassword = "oldPassword123",
                NewPassword = "newPassword123",
                ConfirmPassword = "differentPassword123"
            };

            var principal = new ClaimsPrincipal();
            var user = new ApplicationUser();

            mockUserManager.Setup(m => m.GetUserAsync(principal)).ReturnsAsync(user);

            var result = await applicationUserService.ChangePasswordWithOldPassword(dto, principal);

            Assert.AreEqual("Паролите не съвпадат!", result);
            mockUserManager.Verify(m => m.GetUserAsync(principal), Times.Once);
        }

        [Test]
        public async Task ChangePasswordWithOldPassword_ShouldReturnErrorWhenChangingPasswords_WhenChangePasswordFails()
        {
            var dto = new ChangePasswordDTO
            {
                OldPassword = "oldPassword123",
                NewPassword = "newPassword123",
                ConfirmPassword = "newPassword123"
            };

            var principal = new ClaimsPrincipal();
            var user = new ApplicationUser();

            mockUserManager.Setup(m => m.GetUserAsync(principal)).ReturnsAsync(user);
            mockUserManager.Setup(m => m.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword))
                           .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

            var result = await applicationUserService.ChangePasswordWithOldPassword(dto, principal);

            Assert.AreEqual("Възникна грешка при смяна на паролата!", result);
            mockUserManager.Verify(m => m.GetUserAsync(principal), Times.Once);
            mockUserManager.Verify(m => m.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword), Times.Once);
        }

        [Test]
        public async Task ChangePasswordWithOldPassword_ShouldReturnChangePasswordSuccess_WhenPasswordChangedSuccessfully()
        {
            var dto = new ChangePasswordDTO
            {
                OldPassword = "oldPassword123",
                NewPassword = "newPassword123",
                ConfirmPassword = "newPassword123"
            };

            var principal = new ClaimsPrincipal();
            var user = new ApplicationUser();

            mockUserManager.Setup(m => m.GetUserAsync(principal)).ReturnsAsync(user);
            mockUserManager.Setup(m => m.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword))
                           .ReturnsAsync(IdentityResult.Success);

            var result = await applicationUserService.ChangePasswordWithOldPassword(dto, principal);

            Assert.AreEqual("Смяната на паролата е успешна.", result);
            mockUserManager.Verify(m => m.GetUserAsync(principal), Times.Once);
            mockUserManager.Verify(m => m.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword), Times.Once);
        }

    }
}
