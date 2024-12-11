using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MockQueryable;
using Moq;
using RentACar.Core.Services;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.Data.Repository.Interfaces;
using RentACar.DTO.Admin;
using RentACar.DTO.Car;
using RentACar.DTO.Role;
using RentACar.DTO.User;
using RentACar.Tests.Helpers;
using System.Security.Claims;
using RentACar.DTO.Result;

namespace RentACar.Tests.ServicesTests
{
    [TestFixture]
    public class AdminServiceTests
    {
        private Mock<IRepository<Car, Guid>> mockCarRepository;
        private Mock<IRepository<Reservation, Guid>> mockReservationRepository;
        private Mock<UserManager<ApplicationUser>> mockUserManager;
        private Mock<RoleManager<IdentityRole<Guid>>> mockRoleManager;
        private Mock<RentACarDbContext> mockContext;
        private Mock<IMapper> mockMapper;
        private AdminService adminService;

        [SetUp]
        public void Setup()
        {
            mockCarRepository = new Mock<IRepository<Car, Guid>>();
            mockReservationRepository = new Mock<IRepository<Reservation, Guid>>();
            mockUserManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object,
                null, null, null, null, null, null, null, null
            );
            mockRoleManager = new Mock<RoleManager<IdentityRole<Guid>>>(
                new Mock<IRoleStore<IdentityRole<Guid>>>().Object,
                null, null, null, null
            );
            mockContext = new Mock<RentACarDbContext>();
            mockMapper = new Mock<IMapper>();

            adminService = new AdminService(
                mockUserManager.Object,
                mockRoleManager.Object,
                mockContext.Object,
                mockCarRepository.Object,
                mockReservationRepository.Object,
                mockMapper.Object
            );
        }

        [Test]
        public async Task GetAppInfo_ShouldReturnCorrectDashboardData()
        {
            List<Car> cars = new List<Car> { new Car(), new Car() };
            List<Reservation> reservations = new List<Reservation> { new Reservation() };
            List<ApplicationUser> users = new List<ApplicationUser>
                { new ApplicationUser(), new ApplicationUser(), new ApplicationUser() };

            mockCarRepository.Setup(repo => repo.GetAllAttached()).Returns(cars.AsQueryable());
            mockReservationRepository.Setup(repo => repo.GetAllAttached()).Returns(reservations.AsQueryable());
            mockUserManager.Setup(um => um.Users).Returns(users.AsQueryable());

            DashboardDTO result = await adminService.GetAppInfo();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.TotalCars);
            Assert.AreEqual(1, result.TotalReservations);
            Assert.AreEqual(3, result.TotalUsers);

            mockCarRepository.Verify(repo => repo.GetAllAttached(), Times.Once);
            mockReservationRepository.Verify(repo => repo.GetAllAttached(), Times.Once);
            mockUserManager.Verify(um => um.Users, Times.Once);
        }

        [Test]
        public async Task GetAppInfo_ShouldReturnZeroCounts_WhenNoDataExists()
        {

            mockCarRepository.Setup(repo => repo.GetAllAttached()).Returns(Enumerable.Empty<Car>().AsQueryable());
            mockReservationRepository.Setup(repo => repo.GetAllAttached())
                .Returns(Enumerable.Empty<Reservation>().AsQueryable());
            mockUserManager.Setup(um => um.Users).Returns(Enumerable.Empty<ApplicationUser>().AsQueryable());


            DashboardDTO result = await adminService.GetAppInfo();


            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.TotalCars);
            Assert.AreEqual(0, result.TotalReservations);
            Assert.AreEqual(0, result.TotalUsers);

            mockCarRepository.Verify(repo => repo.GetAllAttached(), Times.Once);
            mockReservationRepository.Verify(repo => repo.GetAllAttached(), Times.Once);
            mockUserManager.Verify(um => um.Users, Times.Once);
        }

        [Test]
        public async Task GetCarsInformation_ShouldReturnCorrectData()
        {
            List<Car> cars = new List<Car>
            {
                new Car { Id = Guid.NewGuid(), Brand = "BMW", Model = "X5" },
                new Car { Id = Guid.NewGuid(), Brand = "Mercedes", Model = "E-Class" }
            };

            IQueryable<Car>? carsMock = cars.AsQueryable().BuildMock(); // Mock IQueryable with MockQueryable

            List<CarInformationDTO> carDtos = new List<CarInformationDTO>
            {
                new CarInformationDTO { Brand = "BMW", Model = "X5" },
                new CarInformationDTO { Brand = "Mercedes", Model = "E-Class" }
            };

            mockCarRepository.Setup(repo => repo.GetAllAttached()).Returns(carsMock);
            mockMapper.Setup(mapper => mapper.Map<CarInformationDTO>(It.IsAny<Car>()))
                .Returns((Car c) => carDtos.FirstOrDefault(dto => dto.Brand == c.Brand && dto.Model == c.Model));

            IEnumerable<CarInformationDTO> result = await adminService.GetCarsInformation();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("BMW", result.First().Brand);
            Assert.AreEqual("Mercedes", result.Last().Brand);

            mockCarRepository.Verify(repo => repo.GetAllAttached(), Times.Once);
            mockMapper.Verify(mapper => mapper.Map<CarInformationDTO>(It.IsAny<Car>()), Times.Exactly(2));
        }

        [Test]
        public async Task GetCarsInformation_ShouldReturnEmptyList_WhenNoCarsExist()
        {
            IQueryable<Car>? emptyCarsMock = Enumerable.Empty<Car>().AsQueryable().BuildMock();

            mockCarRepository.Setup(repo => repo.GetAllAttached()).Returns(emptyCarsMock);

            IEnumerable<CarInformationDTO> result = await adminService.GetCarsInformation();

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);

            mockCarRepository.Verify(repo => repo.GetAllAttached(), Times.Once);
            mockMapper.Verify(mapper => mapper.Map<CarInformationDTO>(It.IsAny<Car>()), Times.Never);
        }


        [Test]
        public async Task GetReservationsInformation_ShouldReturnMappedManageReservationDTOs()
        {
            IQueryable<Reservation> mockReservations = new List<Reservation>
    {
        new Reservation
        {
            Id = Guid.NewGuid(),
            Car = new Car { Brand = "BMW", Model = "X5" },
            Customer = new ApplicationUser { UserName = "user1" },
            TotalPrice = 100m
        },
        new Reservation
        {
            Id = Guid.NewGuid(),
            Car = new Car { Brand = "Audi", Model = "Q7" },
            Customer = new ApplicationUser { UserName = "user2" },
            TotalPrice = 150m
        }
    }.AsAsyncQueryable();

            Mock<DbSet<Reservation>> mockReservationDbSet = new Mock<DbSet<Reservation>>();

            mockReservationDbSet.As<IQueryable<Reservation>>().Setup(m => m.Provider).Returns(mockReservations.Provider);
            mockReservationDbSet.As<IQueryable<Reservation>>().Setup(m => m.Expression).Returns(mockReservations.Expression);
            mockReservationDbSet.As<IQueryable<Reservation>>().Setup(m => m.ElementType).Returns(mockReservations.ElementType);
            mockReservationDbSet.As<IQueryable<Reservation>>().Setup(m => m.GetEnumerator()).Returns(mockReservations.GetEnumerator());

            mockReservationRepository.Setup(repo => repo.GetAllAttached()).Returns(mockReservationDbSet.Object);

            mockMapper.Setup(m => m.Map<ManageReservationDTO>(It.IsAny<Reservation>()))
                .Returns((Reservation source) => new ManageReservationDTO
                {
                    Id = source.Id.ToString(),
                    CarBrandAndModel = $"{source.Car.Brand} {source.Car.Model}",
                    AccountUsername = source.Customer.UserName,
                    TotalPrice = source.TotalPrice
                });

            AdminService service = new AdminService(
                mockUserManager.Object,
                mockRoleManager.Object,
                mockContext.Object,
                mockCarRepository.Object,
                mockReservationRepository.Object,
                mockMapper.Object);

            IEnumerable<ManageReservationDTO> result = await service.GetReservationsInformation();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());

            ManageReservationDTO? firstReservation = result.FirstOrDefault(r => r.AccountUsername == "user1");
            Assert.IsNotNull(firstReservation);
            Assert.AreEqual("BMW X5", firstReservation.CarBrandAndModel);
            Assert.AreEqual(100m, firstReservation.TotalPrice);

            ManageReservationDTO? secondReservation = result.FirstOrDefault(r => r.AccountUsername == "user2");
            Assert.IsNotNull(secondReservation);
            Assert.AreEqual("Audi Q7", secondReservation.CarBrandAndModel);
            Assert.AreEqual(150m, secondReservation.TotalPrice);

            mockReservationRepository.Verify(repo => repo.GetAllAttached(), Times.Once);
            mockMapper.Verify(m => m.Map<ManageReservationDTO>(It.IsAny<Reservation>()), Times.Exactly(2));
        }

        [Test]
        public async Task IsUserAdmin_ShouldReturnTrue_WhenUserIsAdmin()
        {
            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal();
            ApplicationUser user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "adminUser" };

            mockUserManager.Setup(m => m.GetUserAsync(claimPrincipal))
                .ReturnsAsync(user);

            mockUserManager.Setup(m => m.IsInRoleAsync(user, "Admin"))
                .ReturnsAsync(true);

            bool result = await adminService.IsUserAdmin(claimPrincipal);

            Assert.IsTrue(result);
            mockUserManager.Verify(m => m.GetUserAsync(claimPrincipal), Times.Once);
            mockUserManager.Verify(m => m.IsInRoleAsync(user, "Admin"), Times.Once);
        }

        [Test]
        public async Task IsUserAdmin_ShouldReturnFalse_WhenUserIsNull()
        {
            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal();

            mockUserManager.Setup(m => m.GetUserAsync(claimPrincipal))
                .ReturnsAsync((ApplicationUser)null);

            
            bool result = await adminService.IsUserAdmin(claimPrincipal);

            Assert.IsFalse(result);
            mockUserManager.Verify(m => m.GetUserAsync(claimPrincipal), Times.Once);
            mockUserManager.Verify(m => m.IsInRoleAsync(It.IsAny<ApplicationUser>(), "Admin"), Times.Never);
        }

        [Test]
        public async Task IsUserAdmin_ShouldReturnFalse_WhenUserIsNotAdmin()
        {
            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal();
            ApplicationUser user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "regularUser" };

            mockUserManager.Setup(m => m.GetUserAsync(claimPrincipal))
                .ReturnsAsync(user);

            mockUserManager.Setup(m => m.IsInRoleAsync(user, "Admin"))
                .ReturnsAsync(false);

            bool result = await adminService.IsUserAdmin(claimPrincipal);

            Assert.IsFalse(result);
            mockUserManager.Verify(m => m.GetUserAsync(claimPrincipal), Times.Once);
            mockUserManager.Verify(m => m.IsInRoleAsync(user, "Admin"), Times.Once);
        }

        [Test]
        public async Task IsUserModerator_ShouldReturnTrue_WhenUserIsModerator()
        {
            ApplicationUser user = new ApplicationUser();
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();

            mockUserManager.Setup(m => m.GetUserAsync(claimsPrincipal)).ReturnsAsync(user);
            mockUserManager.Setup(m => m.IsInRoleAsync(user, "Moderator")).ReturnsAsync(true);

            bool result = await adminService.IsUserModerator(claimsPrincipal);

            Assert.IsTrue(result);
            mockUserManager.Verify(m => m.GetUserAsync(claimsPrincipal), Times.Once);
            mockUserManager.Verify(m => m.IsInRoleAsync(user, "Moderator"), Times.Once);
        }

        [Test]
        public async Task IsUserModerator_ShouldReturnFalse_WhenUserIsNull()
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();

            mockUserManager.Setup(m => m.GetUserAsync(claimsPrincipal)).ReturnsAsync((ApplicationUser)null);

            bool result = await adminService.IsUserModerator(claimsPrincipal);

            Assert.IsFalse(result);
            mockUserManager.Verify(m => m.GetUserAsync(claimsPrincipal), Times.Once);
            mockUserManager.Verify(m => m.IsInRoleAsync(It.IsAny<ApplicationUser>(), "Moderator"), Times.Never);
        }

        [Test]
        public async Task IsUserModerator_ShouldReturnFalse_WhenUserIsNotModerator()
        {
            ApplicationUser user = new ApplicationUser();
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();

            mockUserManager.Setup(m => m.GetUserAsync(claimsPrincipal)).ReturnsAsync(user);
            mockUserManager.Setup(m => m.IsInRoleAsync(user, "Moderator")).ReturnsAsync(false);

            bool result = await adminService.IsUserModerator(claimsPrincipal);

            Assert.IsFalse(result);
            mockUserManager.Verify(m => m.GetUserAsync(claimsPrincipal), Times.Once);
            mockUserManager.Verify(m => m.IsInRoleAsync(user, "Moderator"), Times.Once);
        }

        [Test]
        public async Task SetRoleToUser_ShouldReturnInvalidGuidResult_WhenUserIdIsInvalid()
        {
            SetRoleDTO dto = new SetRoleDTO { UserId = "invalid-guid", RoleName = "Admin" };
            ClaimsPrincipal claim = new ClaimsPrincipal();

            Result result = await adminService.SetRoleToUser(dto, claim);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Невалидно Id", result.Message);
        }

        [Test]
        public async Task SetRoleToUser_ShouldReturnInvalidUserIdResult_WhenUserNotFound()
        {
            SetRoleDTO dto = new SetRoleDTO { UserId = Guid.NewGuid().ToString(), RoleName = "Admin" };
            ClaimsPrincipal claim = new ClaimsPrincipal();

            mockUserManager.Setup(m => m.FindByIdAsync(dto.UserId)).ReturnsAsync((ApplicationUser)null);

            Result result = await adminService.SetRoleToUser(dto, claim);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Няма потребител с това Id", result.Message);
        }

        [Test]
        public async Task SetRoleToUser_ShouldReturnInvalidRoleIdResult_WhenRoleNotFound()
        {
            SetRoleDTO dto = new SetRoleDTO { UserId = Guid.NewGuid().ToString(), RoleName = "InvalidRole" };
            ClaimsPrincipal claim = new ClaimsPrincipal();
            ApplicationUser user = new ApplicationUser();

            mockUserManager.Setup(m => m.FindByIdAsync(dto.UserId)).ReturnsAsync(user);
            mockRoleManager.Setup(m => m.FindByNameAsync(dto.RoleName)).ReturnsAsync((IdentityRole<Guid>)null);

            Result result = await adminService.SetRoleToUser(dto, claim);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Няма роля с това Id", result.Message);
        }

        [Test]
        public async Task SetRoleToUser_ShouldReturnCannotModifyOwnRoleResult_WhenModifyingOwnRole()
        {
            SetRoleDTO dto = new SetRoleDTO { UserId = Guid.NewGuid().ToString(), RoleName = "Admin" };
            ClaimsPrincipal claim = new ClaimsPrincipal();
            ApplicationUser user = new ApplicationUser();
            IdentityRole<Guid> role = new IdentityRole<Guid>();

            mockUserManager.Setup(m => m.FindByIdAsync(dto.UserId)).ReturnsAsync(user);
            mockRoleManager.Setup(m => m.FindByNameAsync(dto.RoleName)).ReturnsAsync(role);
            mockUserManager.Setup(m => m.GetUserAsync(claim)).ReturnsAsync(user);

            Result result = await adminService.SetRoleToUser(dto, claim);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Не може да пипаш нищо по своя акаунт.", result.Message);
        }

        [Test]
        public async Task SetRoleToUser_ShouldReturnCannotModifySuperAdmin_WhenUserHasSuperAdminClaim()
        {
            SetRoleDTO dto = new SetRoleDTO { UserId = Guid.NewGuid().ToString(), RoleName = "Admin" };
            ClaimsPrincipal claim = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            }));

            ApplicationUser user = new ApplicationUser();
            IdentityRole<Guid> role = new IdentityRole<Guid> { Name = "Admin" };

            mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            mockUserManager.Setup(m => m.GetClaimsAsync(user)).ReturnsAsync(new List<Claim>
            {
                new Claim("SuperAdminClaimType", "true")
            });

            mockRoleManager.Setup(m => m.FindByNameAsync(dto.RoleName)).ReturnsAsync(role);
            mockUserManager.Setup(m => m.FindByIdAsync(dto.UserId)).ReturnsAsync(user);
            mockUserManager.Setup(m => m.AddToRoleAsync(user, role.Name)).ReturnsAsync(IdentityResult.Success);

            
            Result result = await adminService.SetRoleToUser(dto, claim);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Не може да пипаш нищо по своя акаунт.", result.Message);

        }

        [Test]
        public async Task DeleteRoleFromUser_ShouldReturnInvalidGuidResult_WhenUserIdIsInvalid()
        {
            SetRoleDTO dto = new SetRoleDTO { UserId = "invalid-guid", RoleName = "Admin" };
            ClaimsPrincipal claim = new ClaimsPrincipal();

            Result result = await adminService.DeleteRoleFromUser(dto, claim);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Невалидно Id", result.Message);
        }

        [Test]
        public async Task DeleteRoleFromUser_ShouldReturnCannotModifyOwnRole_WhenModifyingOwnRole()
        {
            SetRoleDTO dto = new SetRoleDTO { UserId = Guid.NewGuid().ToString(), RoleName = "Admin" };
            ClaimsPrincipal claim = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, dto.UserId)
        }));

            Result result = await adminService.DeleteRoleFromUser(dto, claim);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Не може да пипаш нищо по своя акаунт.", result.Message);
        }

        [Test]
        public async Task DeleteRoleFromUser_ShouldReturnInvalidUserIdResult_WhenUserNotFound()
        {
            SetRoleDTO dto = new SetRoleDTO { UserId = Guid.NewGuid().ToString(), RoleName = "Admin" };
            ClaimsPrincipal claim = new ClaimsPrincipal();

            mockUserManager.Setup(m => m.GetUserAsync(claim)).ReturnsAsync(new ApplicationUser { Id = Guid.NewGuid() });
            mockUserManager.Setup(m => m.FindByIdAsync(dto.UserId)).ReturnsAsync((ApplicationUser)null);

            Result result = await adminService.DeleteRoleFromUser(dto, claim);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Няма потребител с това Id", result.Message);
        }


        [Test]
        public async Task DeleteRoleFromUser_ShouldReturnInvalidRoleIdResult_WhenRoleNotFound()
        {
            SetRoleDTO dto = new SetRoleDTO { UserId = Guid.NewGuid().ToString(), RoleName = "InvalidRole" };
            ClaimsPrincipal claim = new ClaimsPrincipal();
            ApplicationUser user = new ApplicationUser();

            mockUserManager.Setup(m => m.FindByIdAsync(dto.UserId)).ReturnsAsync(user);
            mockUserManager.Setup(m => m.GetClaimsAsync(user)).ReturnsAsync(new List<Claim>());
            mockRoleManager.Setup(m => m.FindByNameAsync(dto.RoleName)).ReturnsAsync((IdentityRole<Guid>)null);

            Result result = await adminService.DeleteRoleFromUser(dto, claim);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Няма роля с това Id", result.Message);
        }

        [Test]
        public async Task DeleteRoleFromUser_ShouldReturnSuccessResult_WhenRoleRemovedSuccessfully()
        {
            SetRoleDTO dto = new SetRoleDTO { UserId = Guid.NewGuid().ToString(), RoleName = "Admin" };
            ClaimsPrincipal claim = new ClaimsPrincipal();
            ApplicationUser user = new ApplicationUser();
            IdentityRole<Guid> role = new IdentityRole<Guid> { Name = "Admin" };

            mockUserManager.Setup(m => m.FindByIdAsync(dto.UserId)).ReturnsAsync(user);
            mockUserManager.Setup(m => m.GetClaimsAsync(user)).ReturnsAsync(new List<Claim>());
            mockRoleManager.Setup(m => m.FindByNameAsync(dto.RoleName)).ReturnsAsync(role);
            mockUserManager.Setup(m => m.RemoveFromRoleAsync(user, dto.RoleName)).ReturnsAsync(IdentityResult.Success);

            Result result = await adminService.DeleteRoleFromUser(dto, claim);

            Assert.IsTrue(result.Success);
            Assert.AreEqual("Successfull", result.Message);

            mockUserManager.Verify(m => m.RemoveFromRoleAsync(user, dto.RoleName), Times.Once);
        }

        [Test]
        public async Task DeleteUser_ShouldReturnInvalidGuidResult_WhenIdIsInvalid()
        {
            DeleteUserDTO dto = new DeleteUserDTO { Id = "invalid-guid" };
            ClaimsPrincipal claim = new ClaimsPrincipal();

            Result result = await adminService.DeleteUser(dto, claim);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Невалидно Id", result.Message);
        }

        [Test]
        public async Task DeleteUser_ShouldReturnCannotModifyOwnRole_WhenModifyingOwnRole()
        {
            DeleteUserDTO dto = new DeleteUserDTO() { Id = Guid.NewGuid().ToString()};
            ClaimsPrincipal claim = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, dto.Id)
            }));

            Result result = await adminService.DeleteUser(dto, claim);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Не може да пипаш нищо по своя акаунт.", result.Message);
        }

        [Test]
        public async Task DeleteUser_ShouldReturnSuccess_WhenUserDeletedSuccessfully()
        {
            DeleteUserDTO dto = new DeleteUserDTO { Id = Guid.NewGuid().ToString() };
            ClaimsPrincipal claim = new ClaimsPrincipal();
            ApplicationUser user = new ApplicationUser { Id = Guid.NewGuid() };

            mockUserManager.Setup(m => m.FindByIdAsync(dto.Id)).ReturnsAsync(user);
            mockUserManager.Setup(m => m.GetClaimsAsync(user)).ReturnsAsync(new List<Claim>());

            mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            Result result = await adminService.DeleteUser(dto, claim);

            Assert.IsTrue(result.Success);
            Assert.AreEqual("Successfull", result.Message);
        }

        [Test]
        public async Task DeleteUser_ShouldReturnErrorWhenDeletingUser_WhenExceptionThrown()
        {
            DeleteUserDTO dto = new DeleteUserDTO { Id = Guid.NewGuid().ToString() };
            ClaimsPrincipal claim = new ClaimsPrincipal();
            ApplicationUser user = new ApplicationUser { Id = Guid.NewGuid() };

            mockUserManager.Setup(m => m.FindByIdAsync(dto.Id)).ReturnsAsync(user);
            mockUserManager.Setup(m => m.GetClaimsAsync(user)).ReturnsAsync(new List<Claim>());

            mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

            Result result = await adminService.DeleteUser(dto, claim);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Възникна грешка при изтриването на потребителя.", result.Message);
        }
    }
}
