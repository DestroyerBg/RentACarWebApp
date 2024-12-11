using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using RentACar.Core.Services;
using RentACar.Data.Repository;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.DTO.Car;
using RentACar.DTO.CustomerFeedback;
using RentACar.Data.Repository.Interfaces;
using System.Security.Claims;
using static RentACar.Common.Messages.DatabaseModelsMessages.CustomerFeedback;
namespace RentACar.Tests.ServicesTests
{
    [TestFixture]
    public class CustomerFeedbackServiceTests
    {
        private DbContextOptions<RentACarDbContext> _options;
        private IMapper _mapper;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        [SetUp]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<RentACarDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Car, FeedbackCarDTO>();
                cfg.CreateMap<SendFeedbackDTO, CustomerFeedback>();
                cfg.CreateMap<CustomerFeedback, UserFeedbackDTO>()
                    .ForMember(dest => dest.CarBrandAndModel, opt => opt.MapFrom(src => src.Car.Brand))
                    .ForMember(dest => dest.CustomerUsername, opt => opt.MapFrom(src => src.Customer.UserName));
            });
            _mapper = config.CreateMapper();

            _userManagerMock = GetMockUserManager();
        }

        private Mock<UserManager<ApplicationUser>> GetMockUserManager()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(
                store.Object,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null
            );
        }

        [Test]
        public async Task CreateSendFeedbackDTO_ShouldReturnDtoWithCars_WhenCarsExist()
        {
            using var context = new RentACarDbContext(_options);

            var carRepository = new BaseRepository<Car, Guid>(context);
            var feedbackRepository = new BaseRepository<CustomerFeedback, Guid>(context);

            var cars = new List<Car>
            {
                new Car { Id = Guid.NewGuid(), Brand = "Toyota", Model = "Corolla", ImageUrl = "Photo", RegistrationNumber = "СН6123ТХ"  },
                new Car { Id = Guid.NewGuid(), Brand = "Ford", Model = "Focus", ImageUrl = "Photo", RegistrationNumber = "СН6123ТХ" }
            };

            context.Cars.AddRange(cars);
            await context.SaveChangesAsync();

            var userManagerMock = GetMockUserManager();

            var customerFeedbackService = new CustomerFeedbackService(
                feedbackRepository,
                userManagerMock.Object,
                carRepository,
                _mapper
            );

            var result = await customerFeedbackService.CreateSendFeedbackDTO();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Cars);
            Assert.AreEqual(2, result.Cars.Count);

        }


        [Test]
        public async Task CreateSendFeedbackDTO_ShouldReturnEmptyDto_WhenNoCarsExist()
        {
            using var context = new RentACarDbContext(_options);

            var carRepository = new BaseRepository<Car, Guid>(context);
            var feedbackRepository = new BaseRepository<CustomerFeedback, Guid>(context);

            var userManagerMock = GetMockUserManager();

            var customerFeedbackService = new CustomerFeedbackService(
                feedbackRepository,
                userManagerMock.Object,
                carRepository,
                _mapper
            );

            var result = await customerFeedbackService.CreateSendFeedbackDTO();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Cars);
            Assert.IsEmpty(result.Cars);
        }

        [Test]
        public async Task CreateFeedback_ShouldReturnSuccess_WhenDataIsValid()
        {
            using var context = new RentACarDbContext(_options);

            var feedbackRepository = new BaseRepository<CustomerFeedback, Guid>(context);

            var customerFeedbackService = new CustomerFeedbackService(
                feedbackRepository,
                _userManagerMock.Object,
                Mock.Of<IRepository<Car, Guid>>(),
                _mapper
            );

            var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "TestUser" };

            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(user);

            var feedbackDto = new SendFeedbackDTO
            {
                Rating = 4,
                CarId = Guid.NewGuid().ToString(),
                Description = "Great car!"
            };

            var claim = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim("Test","Test")
                    },
                    "TestAuthentication"
                )
            );

            var result = await customerFeedbackService.CreateFeedback(feedbackDto, claim);

            Assert.IsTrue(result.Success);

            var feedback = await context.CustomerFeedbacks.FirstOrDefaultAsync();
            Assert.IsNotNull(feedback);
            Assert.AreEqual(user.Id, feedback.CustomerId);
            Assert.AreEqual(4, feedback.Rating);
            Assert.AreEqual("Great car!", feedback.Description);
        }

        [Test]
        public async Task CreateFeedback_ShouldReturnError_WhenRatingIsInvalid()
        {
            using var context = new RentACarDbContext(_options);

            var feedbackRepository = new BaseRepository<CustomerFeedback, Guid>(context);

            var customerFeedbackService = new CustomerFeedbackService(
                feedbackRepository,
                _userManagerMock.Object,
                Mock.Of<IRepository<Car, Guid>>(),
                _mapper
            );

            var claim = new ClaimsPrincipal();

            var feedbackDto = new SendFeedbackDTO
            {
                Rating = 10,
                CarId = Guid.NewGuid().ToString(),
                Description = "Great car!"
            };

            var result = await customerFeedbackService.CreateFeedback(feedbackDto, claim);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(RatingValuesError, result.Message);

            var feedbackCount = await context.CustomerFeedbacks.CountAsync();
            Assert.AreEqual(0, feedbackCount);
        }

        [Test]
        public async Task CreateFeedback_ShouldReturnError_WhenUserIsNotLoggedIn()
        {
            using var context = new RentACarDbContext(_options);

            var feedbackRepository = new BaseRepository<CustomerFeedback, Guid>(context);

            var customerFeedbackService = new CustomerFeedbackService(
                feedbackRepository,
                _userManagerMock.Object,
                Mock.Of<IRepository<Car, Guid>>(),
                _mapper
            );

            var claim = new ClaimsPrincipal();

            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync((ApplicationUser)null);

            var feedbackDto = new SendFeedbackDTO
            {
                Rating = 4,
                CarId = Guid.NewGuid().ToString(),
                Description = "Great car!"
            };

            var result = await customerFeedbackService.CreateFeedback(feedbackDto, claim);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(OnlyLoggedInUsersCanSendFeedback, result.Message);

            var feedbackCount = await context.CustomerFeedbacks.CountAsync();
            Assert.AreEqual(0, feedbackCount);
        }

        [Test]
        public async Task GetAllFeedbacks_ShouldReturnAllFeedbacks_WhenFeedbacksExist()
        {
            using var context = new RentACarDbContext(_options);

            var car = new Car { Id = Guid.NewGuid(), Brand = "Toyota", ImageUrl = "/PhotoPath", Model = "Corolla", RegistrationNumber = "ТХ1232СН"};
            var customer = new ApplicationUser { Id = Guid.NewGuid(), UserName = "TestUser", FirstName = "Pesho", LastName = "Peshev"};
            var feedbacks = new List<CustomerFeedback>
        {
            new CustomerFeedback
            {
                Id = Guid.NewGuid(),
                Rating = 5,
                Description = "Great service!",
                Car = car,
                Customer = customer
            },
            new CustomerFeedback
            {
                Id = Guid.NewGuid(),
                Rating = 4,
                Description = "Good service!",
                Car = car,
                Customer = customer
            }
        };

            context.Cars.Add(car);
            context.Users.Add(customer);
            context.CustomerFeedbacks.AddRange(feedbacks);
            await context.SaveChangesAsync();

            var feedbackRepository = new BaseRepository<CustomerFeedback, Guid>(context);

            var customerFeedbackService = new CustomerFeedbackService(
                feedbackRepository,
                _userManagerMock.Object,
                Mock.Of<IRepository<Car, Guid>>(),
                _mapper
            );

            var result = await customerFeedbackService.GetAllFeedbacks();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());

            var feedbackList = result.ToList();

            Assert.AreEqual("Toyota", feedbackList[0].CarBrandAndModel);
            Assert.AreEqual("TestUser", feedbackList[0].CustomerUsername);
            Assert.AreEqual("Great service!", feedbackList[0].Description);

            Assert.AreEqual("Toyota", feedbackList[1].CarBrandAndModel);
            Assert.AreEqual("TestUser", feedbackList[1].CustomerUsername);
            Assert.AreEqual("Good service!", feedbackList[1].Description);
        }

        [Test]
        public async Task GetAllFeedbacks_ShouldReturnEmptyList_WhenNoFeedbacksExist()
        {
            using var context = new RentACarDbContext(_options);

            var feedbackRepository = new BaseRepository<CustomerFeedback, Guid>(context);

            var customerFeedbackService = new CustomerFeedbackService(
                feedbackRepository,
                _userManagerMock.Object,
                Mock.Of<IRepository<Car, Guid>>(),
                _mapper
            );

            var result = await customerFeedbackService.GetAllFeedbacks();

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }
    }
}
