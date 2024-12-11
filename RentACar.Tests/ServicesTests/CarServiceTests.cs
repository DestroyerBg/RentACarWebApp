using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using RentACar.Core.Interfaces;
using RentACar.Core.Services;
using RentACar.Data.Models;
using RentACar.Data.Repository.Interfaces;
using RentACar.DTO.Car;
using RentACar.Tests.Helpers;
using MockQueryable;
using RentACar.Data.Repository;
using RentACar.Data;
using RentACar.DTO.Category;
using RentACar.DTO.Feature;
using RentACar.DTO.InsuranceBenefit;
using RentACar.DTO.Location;
using RentACar.DTO.Reservation;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentACar.DTO.Result;
using static RentACar.Common.Messages.DatabaseModelsMessages.Car;
using static RentACar.Common.Messages.DatabaseModelsMessages.Common;
namespace RentACar.Tests.ServicesTests
{
    [TestFixture]
    public class CarServiceTests
    {

        private Mock<IRepository<Car, Guid>> _carRepositoryMock;
        private Mock<IRepository<InsuranceBenefit, Guid>> _insuranceBenefitRepositoryMock;
        private Mock<IRepository<Location, Guid>> _locationRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private CarService _carService;
        private Mock<ReservationService> _reservationServiceMock;
        private Mock<CarService> _carServiceMock;
        private DbContextOptions<RentACarDbContext> _options;
        private IMapper _mapper;
        private IMock<IFileService> _fileServiceMock;

        [SetUp]
        public void SetUp()
        {
            _carRepositoryMock = new Mock<IRepository<Car, Guid>>();
            _insuranceBenefitRepositoryMock = new Mock<IRepository<InsuranceBenefit, Guid>>();
            _locationRepositoryMock = new Mock<IRepository<Location, Guid>>();
            _mapperMock = new Mock<IMapper>();

            _carService = new CarService(
                _carRepositoryMock.Object,
                _mapperMock.Object,
                _insuranceBenefitRepositoryMock.Object,
                _locationRepositoryMock.Object,
                Mock.Of<IRepository<Category, Guid>>(),
                Mock.Of<IRepository<Feature, Guid>>(),
                Mock.Of<IFileService>()
            );

            _reservationServiceMock = new Mock<ReservationService>(
                _carRepositoryMock.Object,
                _locationRepositoryMock.Object,
                _mapperMock.Object
            );

            _carServiceMock = new Mock<CarService>(
                    _carRepositoryMock.Object,
                    _mapperMock.Object,
                    _insuranceBenefitRepositoryMock.Object,
                    _locationRepositoryMock.Object,
                    Mock.Of<IRepository<Category, Guid>>(),
                    Mock.Of<IRepository<Feature, Guid>>(),
                    Mock.Of<IFileService>()
                )
                { CallBase = true };

            _options = new DbContextOptionsBuilder<RentACarDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDTO>();
                cfg.CreateMap<Location, LocationDTO>();
                cfg.CreateMap<Feature, FeatureCheckboxDTO>();
                cfg.CreateMap<AddCarDTO, Car>();
                cfg.CreateMap<EditCarDTO, Car>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
            });
            _mapper = config.CreateMapper();

            _fileServiceMock = new Mock<IFileService>();

        }

        [Test]
        public async Task GetCarsAsync_ShouldReturnMappedCars()
        {
            Car car1 = new Car
            {
                Id = Guid.NewGuid(),
                Brand = "Toyota",
                Model = "Corolla",
                HorsePower = 120,
                RegistrationNumber = "123-ABC",
                YearOfManufacture = 2020,
                Location = new Location { City = "Sofia" },
                ImageUrl = "http://image.url/toyota.jpg",
                PricePerDay = 50,
                IsHired = false,
                CarFeatures = new List<CarFeature>
                {
                    new CarFeature { Feature = new Feature { Name = "Air Conditioning" } }
                }
            };

            Car car2 = new Car
            {
                Id = Guid.NewGuid(),
                Brand = "Ford",
                Model = "Focus",
                HorsePower = 150,
                RegistrationNumber = "456-DEF",
                YearOfManufacture = 2019,
                Location = new Location { City = "Plovdiv" },
                ImageUrl = "http://image.url/ford.jpg",
                PricePerDay = 60,
                IsHired = true,
                CarFeatures = new List<CarFeature>
                {
                    new CarFeature { Feature = new Feature { Name = "GPS" } }
                }
            };

            IQueryable<Car> carList = new List<Car> { car1, car2 }.AsAsyncQueryable();

            Mock<DbSet<Car>> dbSetMock = new Mock<DbSet<Car>>();
            dbSetMock.As<IQueryable<Car>>().Setup(m => m.Provider).Returns(carList.Provider);
            dbSetMock.As<IQueryable<Car>>().Setup(m => m.Expression).Returns(carList.Expression);
            dbSetMock.As<IQueryable<Car>>().Setup(m => m.ElementType).Returns(carList.ElementType);
            dbSetMock.As<IQueryable<Car>>().Setup(m => m.GetEnumerator()).Returns(carList.GetEnumerator());

            _carRepositoryMock.Setup(repo => repo.GetAllAttached()).Returns(dbSetMock.Object);

            _mapperMock.Setup(m => m.Map<Car, ViewCarDTO>(It.IsAny<Car>()))
                .Returns((Car car) => new ViewCarDTO
                {
                    Id = car.Id,
                    Brand = car.Brand,
                    Model = car.Model,
                    HorsePower = car.HorsePower,
                    RegistrationNumber = car.RegistrationNumber,
                    YearOfManufacture = car.YearOfManufacture,
                    Location = car.Location.City,
                    ImageUrl = car.ImageUrl,
                    PricePerDay = car.PricePerDay,
                    IsHired = car.IsHired,
                    Features = car.CarFeatures.Select(cf => new FeatureDTO { Name = cf.Feature.Name }).ToList()
                });

            IEnumerable<ViewCarDTO> result = await _carService.GetCarsAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());

            ViewCarDTO firstCar = result.First();
            Assert.AreEqual("Toyota", firstCar.Brand);
            Assert.AreEqual("Sofia", firstCar.Location);
            Assert.AreEqual(1, firstCar.Features.Count);
            Assert.AreEqual("Air Conditioning", firstCar.Features.First().Name);

            ViewCarDTO secondCar = result.Last();
            Assert.AreEqual("Ford", secondCar.Brand);
            Assert.AreEqual("Plovdiv", secondCar.Location);
            Assert.AreEqual(1, secondCar.Features.Count);
            Assert.AreEqual("GPS", secondCar.Features.First().Name);
        }

        [Test]
        public async Task GetCarsFilteredByPriceAsync_ShouldReturnFilteredCars_WhenPriceIsValid()
        {

            Car car1 = new Car
            {
                Id = Guid.NewGuid(),
                Brand = "Toyota",
                Model = "Corolla",
                PricePerDay = 50,
                CarFeatures = new List<CarFeature>
                    { new CarFeature { Feature = new Feature { Name = "Air Conditioning" } } }
            };

            Car car2 = new Car
            {
                Id = Guid.NewGuid(),
                Brand = "Ford",
                Model = "Focus",
                PricePerDay = 100,
                CarFeatures = new List<CarFeature> { new CarFeature { Feature = new Feature { Name = "GPS" } } }
            };

            IQueryable<Car> carList = new List<Car> { car1, car2 }.AsAsyncQueryable();

            Mock<DbSet<Car>> dbSetMock = new Mock<DbSet<Car>>();
            dbSetMock.As<IQueryable<Car>>().Setup(m => m.Provider).Returns(carList.Provider);
            dbSetMock.As<IQueryable<Car>>().Setup(m => m.Expression).Returns(carList.Expression);
            dbSetMock.As<IQueryable<Car>>().Setup(m => m.ElementType).Returns(carList.ElementType);
            dbSetMock.As<IQueryable<Car>>().Setup(m => m.GetEnumerator()).Returns(carList.GetEnumerator());

            _carRepositoryMock.Setup(repo => repo.GetAllAttached()).Returns(dbSetMock.Object);

            _mapperMock.Setup(m => m.Map<Car, ViewCarDTO>(It.IsAny<Car>()))
                .Returns((Car car) => new ViewCarDTO
                {
                    Id = car.Id,
                    Brand = car.Brand,
                    Model = car.Model,
                    PricePerDay = car.PricePerDay,
                    Features = car.CarFeatures.Select(cf => new FeatureDTO { Name = cf.Feature.Name }).ToList()
                });

            IEnumerable<ViewCarDTO> result = await _carService.GetCarsFilteredByPriceAsync("60");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Toyota", result.First().Brand);
        }

        [Test]
        public async Task GetCarsFilteredByPriceAsync_ShouldReturnNull_WhenPriceIsInvalid()
        {
            IEnumerable<ViewCarDTO> result = await _carService.GetCarsFilteredByPriceAsync("invalid");

            Assert.IsNull(result);
        }

        [Test]
        public async Task ReserveACar_CarDoesNotExist_ReturnsNull()
        {
            Guid carId = Guid.NewGuid();
            _carRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Car>().AsQueryable().BuildMock());

            RentACarDTO result = await _carService.ReserveACar(carId);

            Assert.IsNull(result);
        }

        [Test]
        public async Task ReserveACar_ShouldReturnCarDto_WhenCarExists_UsingInMemoryDb()
        {
            
            DbContextOptions<RentACarDbContext> options = new DbContextOptionsBuilder<RentACarDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using RentACarDbContext dbContext = new RentACarDbContext(options);

            Guid carId = Guid.NewGuid();
            Location location = new Location { Id = Guid.NewGuid(), City = "Sofia" };
            Car car = new Car
            {
                Id = carId,
                Brand = "Toyota",
                Model = "Corolla",
                Location = location,
                RegistrationNumber = "С7110ТХ",
                ImageUrl = "/Images/Snimka"
            };

            List<InsuranceBenefit> insuranceBenefits = new List<InsuranceBenefit>
    {
        new InsuranceBenefit { Id = Guid.NewGuid(), Name = "Full Coverage", IconClass = "Ikonka"},
        new InsuranceBenefit { Id = Guid.NewGuid(), Name = "Third Party", IconClass = "Ikonka"}
    };

            List<Location> locations = new List<Location>
    {
        new Location { Id = Guid.NewGuid(), City = "Plovdiv" }
    };

            dbContext.Cars.Add(car);
            dbContext.InsuranceBenefits.AddRange(insuranceBenefits);
            dbContext.Locations.AddRange(locations);
            await dbContext.SaveChangesAsync();

            BaseRepository<Car, Guid> carRepository = new BaseRepository<Car, Guid>(dbContext);
            BaseRepository<InsuranceBenefit, Guid> insuranceBenefitRepository = new BaseRepository<InsuranceBenefit, Guid>(dbContext);
            BaseRepository<Location, Guid> locationRepository = new BaseRepository<Location, Guid>(dbContext);

            Mock<IMapper> mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<Car, RentACarDTO>(It.IsAny<Car>()))
                .Returns((Car c) => new RentACarDTO
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    City = c.Location.City
                });

            mapperMock.Setup(m => m.Map<InsuranceBenefit, InsuranceBenefitDTO>(It.IsAny<InsuranceBenefit>()))
                .Returns((InsuranceBenefit ib) => new InsuranceBenefitDTO { Name = ib.Name });

            mapperMock.Setup(m => m.Map<LocationDTO>(It.IsAny<Location>()))
                .Returns((Location l) => new LocationDTO { City = l.City });

            CarService carService = new CarService(
                carRepository,
                mapperMock.Object,
                insuranceBenefitRepository,
                locationRepository,
                Mock.Of<IRepository<Category, Guid>>(),
                Mock.Of<IRepository<Feature, Guid>>(),
                Mock.Of<IFileService>()
            );

            RentACarDTO result = await carService.ReserveACar(carId);

            Assert.IsNotNull(result);
            Assert.AreEqual("Toyota", result.Brand);
            Assert.AreEqual("Corolla", result.Model);
            Assert.AreEqual("Sofia", result.City);
            Assert.AreEqual(2, result.Benefits.Count);
            Assert.AreEqual(2, result.Locations.Count);
        }

        [Test]
        public async Task CreateReservationConfirmation_ShouldReturnConfirmation_WhenDataIsValid()
        {
            // Arrange
            const string DateFormat = "dd.MM.yyyy";
            string carId = Guid.NewGuid().ToString();
            string locationId = Guid.NewGuid().ToString();

            Car car = new Car
            {
                Id = Guid.Parse(carId),
                Brand = "Toyota",
                Model = "Corolla"
            };

            Location location = new Location
            {
                Id = Guid.Parse(locationId),
                City = "Sofia"
            };

            CreateReservationDTO reservationDTO = new CreateReservationDTO
            {
                CarId = carId,
                LocationId = locationId,
                StartDate = DateTime.Now.ToString(DateFormat), // Форматирана дата
                EndDate = DateTime.Now.AddDays(5).ToString(DateFormat), // Форматирана дата
                Address = "123 Test St.",
                CustomerId = "C123",
                PhoneNumber = "123456789"
            };

            ConfirmReservationDTO expectedConfirmation = new ConfirmReservationDTO
            {
                CarId = carId,
                LocationId = locationId,
                StartDate = DateTime.ParseExact(reservationDTO.StartDate, DateFormat, null),
                EndDate = DateTime.ParseExact(reservationDTO.EndDate, DateFormat, null),
                Address = reservationDTO.Address,
                PhoneNumber = reservationDTO.PhoneNumber,
                CustomerId = reservationDTO.CustomerId,
                CarBrand = car.Brand,
                CarModel = car.Model,
                TotalPrice = 500 // Симулирана стойност
            };

            _carRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(car);

            _locationRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(location);

            _mapperMock.Setup(m => m.Map<ConfirmReservationDTO>(It.IsAny<CreateReservationDTO>()))
                .Returns((CreateReservationDTO dto) => new ConfirmReservationDTO
                {
                    CarId = dto.CarId,
                    LocationId = dto.LocationId,
                    StartDate = DateTime.ParseExact(dto.StartDate, DateFormat, null),
                    EndDate = DateTime.ParseExact(dto.EndDate, DateFormat, null),
                    Address = dto.Address,
                    PhoneNumber = dto.PhoneNumber,
                    CustomerId = dto.CustomerId
                });

            // Act
            ConfirmReservationDTO result = await _carService.CreateReservationConfirmation(reservationDTO);


            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedConfirmation.CarBrand, result.CarBrand);
            Assert.AreEqual(expectedConfirmation.CarModel, result.CarModel);
            Assert.AreEqual(expectedConfirmation.StartDate, result.StartDate);
            Assert.AreEqual(expectedConfirmation.EndDate, result.EndDate);
            Assert.AreEqual(expectedConfirmation.LocationId, result.LocationId);
            Assert.AreEqual(expectedConfirmation.CarId, result.CarId);
        }

        [Test]
        public async Task CreateReservationConfirmation_ShouldReturnNull_WhenCarIdIsInvalid()
        {
            const string DateFormat = "dd.MM.yyyy";
            CreateReservationDTO reservationDTO = new CreateReservationDTO
            {
                CarId = "invalid-guid",
                LocationId = Guid.NewGuid().ToString(),
                StartDate = DateTime.Now.ToString(DateFormat), 
                EndDate = DateTime.Now.AddDays(5).ToString(DateFormat),
                Address = "123 Test St.",
                CustomerId = "C123",
                PhoneNumber = "123456789"
            };

            _carServiceMock.Setup(service => service.IsValidGuid(It.IsAny<string>()))
                .Returns(false); 

            ConfirmReservationDTO result = await _carService.CreateReservationConfirmation(reservationDTO);

            Assert.IsNull(result);
        }

        [Test]
        public async Task CreateAddCarDto_ShouldReturnPopulatedDto_WhenDataExists()
        {
            using RentACarDbContext context = new RentACarDbContext(_options);

            List<Category> categories = new List<Category>
        {
            new Category { Id = Guid.NewGuid(), Name = "SUV" },
            new Category { Id = Guid.NewGuid(), Name = "Sedan" }
        };
            List<Location> locations = new List<Location>
        {
            new Location { Id = Guid.NewGuid(), City = "Sofia" },
            new Location { Id = Guid.NewGuid(), City = "Plovdiv" }
        };
            List<Feature> features = new List<Feature>
        {
            new Feature { Id = Guid.NewGuid(), Name = "Air Conditioning" },
            new Feature { Id = Guid.NewGuid(), Name = "GPS" }
        };

            context.Categories.AddRange(categories);
            context.Locations.AddRange(locations);
            context.Features.AddRange(features);
            await context.SaveChangesAsync();

            BaseRepository<Category, Guid> categoryRepository = new BaseRepository<Category, Guid>(context);
            BaseRepository<Location, Guid> locationRepository = new BaseRepository<Location, Guid>(context);
            BaseRepository<Feature, Guid> featureRepository = new BaseRepository<Feature, Guid>(context);

            CarService carService = new CarService(
                Mock.Of<IRepository<Car, Guid>>(),
                _mapper,
                Mock.Of<IRepository<InsuranceBenefit, Guid>>(),
                locationRepository,
                categoryRepository,
                featureRepository,
                Mock.Of<IFileService>()
            );

            AddCarDTO result = await carService.CreateAddCarDto();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Categories.Count);
            Assert.AreEqual(2, result.Locations.Count);
            Assert.AreEqual(2, result.Features.Count);

            Assert.AreEqual("SUV", result.Categories.First().Name);
            Assert.AreEqual("Sofia", result.Locations.First().City);
            Assert.AreEqual("Air Conditioning", result.Features.First().Name);
        }

        [Test]
        public async Task CreateAddCarDto_ShouldReturnEmptyDto_WhenNoDataExists()
        {
            using RentACarDbContext context = new RentACarDbContext(_options);

            BaseRepository<Category, Guid> categoryRepository = new BaseRepository<Category, Guid>(context);
            BaseRepository<Location, Guid> locationRepository = new BaseRepository<Location, Guid>(context);
            BaseRepository<Feature, Guid> featureRepository = new BaseRepository<Feature, Guid>(context);

            CarService carService = new CarService(
                Mock.Of<IRepository<Car, Guid>>(),
                _mapper,
                Mock.Of<IRepository<InsuranceBenefit, Guid>>(),
                locationRepository,
                categoryRepository,
                featureRepository,
                Mock.Of<IFileService>()
            );

            AddCarDTO result = await carService.CreateAddCarDto();

            Assert.IsNotNull(result);
            Assert.IsEmpty(result.Categories);
            Assert.IsEmpty(result.Locations);
            Assert.IsEmpty(result.Features);
        }

        [Test]
        public async Task AddCar_ShouldAddCar_WhenDataIsValid()
        {
            using RentACarDbContext context = new RentACarDbContext(_options);

            BaseRepository<Car, Guid> carRepository = new BaseRepository<Car, Guid>(context);
            BaseRepository<Feature, Guid> featureRepository = new BaseRepository<Feature, Guid>(context);

            Feature feature1 = new Feature { Id = Guid.NewGuid(), Name = "Air Conditioning" };
            Feature feature2 = new Feature { Id = Guid.NewGuid(), Name = "GPS" };
            context.Features.AddRange(feature1, feature2);
            await context.SaveChangesAsync();

            CarService carService = new CarService(
                carRepository,
                _mapper,
                Mock.Of<IRepository<InsuranceBenefit, Guid>>(),
                Mock.Of<IRepository<Location, Guid>>(),
                Mock.Of<IRepository<Category, Guid>>(),
                featureRepository,
                _fileServiceMock.Object
            );

            AddCarDTO carDto = new AddCarDTO
            {
                RegistrationNumber = "123-ABC",
                Brand = "Toyota",
                Model = "Corolla",
                HorsePower = 150,
                YearOfManufacture = 2020,
                Features = new List<FeatureCheckboxDTO>
            {
                new FeatureCheckboxDTO { Id = feature1.Id.ToString(), Name = feature1.Name, IsChecked = true },
                new FeatureCheckboxDTO { Id = feature2.Id.ToString(), Name = feature2.Name, IsChecked = false }
            },
                CarImage = null // Няма изображение
            };

            ResultWithErrors result = await carService.AddCar(carDto);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsEmpty(result.Errors);

        }

        [Test]
        public async Task AddCar_ShouldFail_WhenCarWithSameRegistrationNumberExists()
        {
            using RentACarDbContext context = new RentACarDbContext(_options);

            BaseRepository<Car, Guid> carRepository = new BaseRepository<Car, Guid>(context);
            BaseRepository<Feature, Guid> featureRepository = new BaseRepository<Feature, Guid>(context);

            Car existingCar = new Car
            {
                Id = Guid.NewGuid(),
                RegistrationNumber = "123-ABC",
                Brand = "ExistingBrand",
                Model = "ExistingModel",
                ImageUrl = "/images/photo.jpg"
            };

            context.Cars.Add(existingCar);
            await context.SaveChangesAsync();

            CarService carService = new CarService(
                carRepository,
                _mapper,
                Mock.Of<IRepository<InsuranceBenefit, Guid>>(),
                Mock.Of<IRepository<Location, Guid>>(),
                Mock.Of<IRepository<Category, Guid>>(),
                featureRepository,
                _fileServiceMock.Object
            );

            AddCarDTO carDto = new AddCarDTO
            {
                RegistrationNumber = "123-ABC",
                Brand = "Toyota",
                Model = "Corolla",
                HorsePower = 150,
                YearOfManufacture = 2020,
                Features = new List<FeatureCheckboxDTO>(),
                CarImage = null
            };

            
            ResultWithErrors result = await carService.AddCar(carDto);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public async Task DeleteCarAsync_ShouldReturnTrue_WhenCarExists()
        {
            using RentACarDbContext context = new RentACarDbContext(_options);

            BaseRepository<Car, Guid> carRepository = new BaseRepository<Car, Guid>(context);

            Guid carId = Guid.NewGuid();
            Car car = new Car
            {
                Id = carId,
                Brand = "Toyota",
                Model = "Corolla",
                RegistrationNumber = "123-ABC",
                IsDeleted = false,
                ImageUrl = "/images/photo.jpg"
            };

            context.Cars.Add(car);
            await context.SaveChangesAsync();

            CarService carService = new CarService(
                carRepository,
                _mapper,
                Mock.Of<IRepository<InsuranceBenefit, Guid>>(),
                Mock.Of<IRepository<Location, Guid>>(),
                Mock.Of<IRepository<Category, Guid>>(),
                Mock.Of<IRepository<Feature, Guid>>(),
                Mock.Of<IFileService>()
            );

            bool result = await carService.DeleteCarAsync(carId);
            Assert.IsTrue(result);

        }

        [Test]
        public async Task DeleteCarAsync_ShouldReturnFalse_WhenCarDoesNotExist()
        {
            using RentACarDbContext context = new RentACarDbContext(_options);

            BaseRepository<Car, Guid> carRepository = new BaseRepository<Car, Guid>(context);

            CarService carService = new CarService(
                carRepository,
                _mapper,
                Mock.Of<IRepository<InsuranceBenefit, Guid>>(),
                Mock.Of<IRepository<Location, Guid>>(),
                Mock.Of<IRepository<Category, Guid>>(),
                Mock.Of<IRepository<Feature, Guid>>(),
                Mock.Of<IFileService>()
            );

            Guid nonExistentCarId = Guid.NewGuid();

            bool result = await carService.DeleteCarAsync(nonExistentCarId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task CreateEditCarDto_ShouldReturnDto_WhenCarExists()
        {
            using RentACarDbContext context = new RentACarDbContext(_options);

            Category category = new Category { Id = Guid.NewGuid(), Name = "SUV" };
            Location location = new Location { Id = Guid.NewGuid(), City = "Sofia" };
            Feature feature = new Feature { Id = Guid.NewGuid(), Name = "Air Conditioning" };

            Car car = new Car
            {
                Id = Guid.NewGuid(),
                Brand = "Toyota",
                Model = "Corolla",
                HorsePower = 150,
                PricePerDay = 50,
                YearOfManufacture = 2020,
                RegistrationNumber = "123-ABC",
                ImageUrl = "http://image.url/toyota.jpg",
                CategoryId = category.Id,
                LocationId = location.Id,
                CarFeatures = new List<CarFeature>
            {
                new CarFeature { FeatureId = feature.Id }
            }
            };

            context.Categories.Add(category);
            context.Locations.Add(location);
            context.Features.Add(feature);
            context.Cars.Add(car);
            await context.SaveChangesAsync();

            BaseRepository<Car, Guid> carRepository = new BaseRepository<Car, Guid>(context);
            BaseRepository<Category, Guid> categoryRepository = new BaseRepository<Category, Guid>(context);
            BaseRepository<Location, Guid> locationRepository = new BaseRepository<Location, Guid>(context);
            BaseRepository<Feature, Guid> featureRepository = new BaseRepository<Feature, Guid>(context);

            CarService carService = new CarService(
                carRepository,
                _mapper,
                Mock.Of<IRepository<InsuranceBenefit, Guid>>(),
                locationRepository,
                categoryRepository,
                featureRepository,
                Mock.Of<IFileService>()
            );

            EditCarDTO result = await carService.CreateEditCarDto(car.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(car.Id.ToString(), result.Id);
            Assert.AreEqual("Toyota", result.Brand);
            Assert.AreEqual("Corolla", result.Model);
            Assert.AreEqual(150, result.HorsePower);
            Assert.AreEqual(50, result.PricePerDay);
            Assert.AreEqual(2020, result.YearOfManufacture);
            Assert.AreEqual("123-ABC", result.RegistrationNumber);
            Assert.AreEqual("http://image.url/toyota.jpg", result.CarImageUrl);

            Assert.AreEqual(1, result.Categories.Count);
            Assert.IsTrue(result.Categories.First().Selected);

            Assert.AreEqual(1, result.Locations.Count);
            Assert.IsTrue(result.Locations.First().Selected);

            Assert.AreEqual(1, result.Features.Count);
            Assert.IsTrue(result.Features.First().Selected);
        }

        [Test]
        public async Task CreateEditCarDto_ShouldReturnNull_WhenCarDoesNotExist()
        {
            using RentACarDbContext context = new RentACarDbContext(_options);

            BaseRepository<Car, Guid> carRepository = new BaseRepository<Car, Guid>(context);
            BaseRepository<Category, Guid> categoryRepository = new BaseRepository<Category, Guid>(context);
            BaseRepository<Location, Guid> locationRepository = new BaseRepository<Location, Guid>(context);
            BaseRepository<Feature, Guid> featureRepository = new BaseRepository<Feature, Guid>(context);

            CarService carService = new CarService(
                carRepository,
                _mapper,
                Mock.Of<IRepository<InsuranceBenefit, Guid>>(),
                locationRepository,
                categoryRepository,
                featureRepository,
                Mock.Of<IFileService>()
            );

            Guid nonExistentCarId = Guid.NewGuid();

            EditCarDTO result = await carService.CreateEditCarDto(nonExistentCarId);

            Assert.IsNull(result);
        }

        [Test]
        public async Task EditCarAsync_ShouldEditCar_WhenDataIsValid()
        {
            using RentACarDbContext context = new RentACarDbContext(_options);

            BaseRepository<Car, Guid> carRepository = new BaseRepository<Car, Guid>(context);
            BaseRepository<Feature, Guid> featureRepository = new BaseRepository<Feature, Guid>(context);

            Feature feature1 = new Feature { Id = Guid.NewGuid(), Name = "Air Conditioning" };
            Feature feature2 = new Feature { Id = Guid.NewGuid(), Name = "GPS" };

            Car car = new Car
            {
                Id = Guid.NewGuid(),
                Brand = "Toyota",
                Model = "Corolla",
                RegistrationNumber = "123-ABC",
                CarFeatures = new List<CarFeature> { new CarFeature { FeatureId = feature1.Id } },
                ImageUrl = "http://image.url/old-photo.jpg",
                HorsePower = 120,
                PricePerDay = 40,
                YearOfManufacture = 2020
            };

            context.Features.AddRange(feature1, feature2);
            context.Cars.Add(car);
            await context.SaveChangesAsync();

            CarService carService = new CarService(
                carRepository,
                _mapper,
                Mock.Of<IRepository<InsuranceBenefit, Guid>>(),
                Mock.Of<IRepository<Location, Guid>>(),
                Mock.Of<IRepository<Category, Guid>>(),
                featureRepository,
                _fileServiceMock.Object
            );

            EditCarDTO editCarDto = new EditCarDTO
            {
                Id = car.Id.ToString(),
                Brand = "UpdatedBrand",
                Model = "UpdatedModel",
                RegistrationNumber = "123-ABC",
                HorsePower = 150,
                PricePerDay = 50,
                YearOfManufacture = 2022,
                CarImage = null,
                CarImageUrl = car.ImageUrl,
                Features = new List<SelectListItem>
            {
                new SelectListItem { Value = feature1.Id.ToString(), Selected = false },
                new SelectListItem { Value = feature2.Id.ToString(), Selected = true }
            }
            };


            ResultWithErrors result = await carService.EditCarAsync(editCarDto);

            Assert.IsTrue(result.Success);
            Assert.IsEmpty(result.Errors);

            Car? updatedCar = await context.Cars.Include(c => c.CarFeatures).FirstOrDefaultAsync(c => c.Id == car.Id);
            Assert.IsNotNull(updatedCar);
            Assert.AreEqual("UpdatedBrand", updatedCar.Brand);
            Assert.AreEqual("UpdatedModel", updatedCar.Model);
            Assert.AreEqual(150, updatedCar.HorsePower);
            Assert.AreEqual(50, updatedCar.PricePerDay);
            Assert.AreEqual(2022, updatedCar.YearOfManufacture);

            Assert.AreEqual(1, updatedCar.CarFeatures.Count);
            Assert.AreEqual(feature2.Id, updatedCar.CarFeatures.First().FeatureId);
        }

        [Test]
        public async Task EditCarAsync_ShouldReturnError_WhenCarIdIsInvalid()
        {
            using RentACarDbContext context = new RentACarDbContext(_options);

            BaseRepository<Car, Guid> carRepository = new BaseRepository<Car, Guid>(context);
            BaseRepository<Feature, Guid> featureRepository = new BaseRepository<Feature, Guid>(context);

            CarService carService = new CarService(
                carRepository,
                _mapper,
                Mock.Of<IRepository<InsuranceBenefit, Guid>>(),
                Mock.Of<IRepository<Location, Guid>>(),
                Mock.Of<IRepository<Category, Guid>>(),
                featureRepository,
                _fileServiceMock.Object
            );

            EditCarDTO invalidDto = new EditCarDTO
            {
                Id = "invalid-guid",
                Brand = "UpdatedBrand",
                Model = "UpdatedModel",
                RegistrationNumber = "123-ABC"
            };

            ResultWithErrors result = await carService.EditCarAsync(invalidDto);

            Assert.IsFalse(result.Success);
        }

        [Test]
        public async Task EditCarAsync_ShouldReturnError_WhenCarDoesNotExist()
        {
            using RentACarDbContext context = new RentACarDbContext(_options);

            BaseRepository<Car, Guid> carRepository = new BaseRepository<Car, Guid>(context);
            BaseRepository<Feature, Guid> featureRepository = new BaseRepository<Feature, Guid>(context);

            CarService carService = new CarService(
                carRepository,
                _mapper,
                Mock.Of<IRepository<InsuranceBenefit, Guid>>(),
                Mock.Of<IRepository<Location, Guid>>(),
                Mock.Of<IRepository<Category, Guid>>(),
                featureRepository,
                _fileServiceMock.Object
            );

            EditCarDTO nonExistentCarDto = new EditCarDTO
            {
                Id = Guid.NewGuid().ToString(),
                Brand = "UpdatedBrand",
                Model = "UpdatedModel",
                RegistrationNumber = "123-ABC"
            };

            ResultWithErrors result = await carService.EditCarAsync(nonExistentCarDto);

            Assert.IsFalse(result.Success);
        }


        [Test]
        public async Task SetCarAsHired_ShouldReturnSuccess_WhenCarExists()
        {
            using RentACarDbContext context = new RentACarDbContext(_options);

            BaseRepository<Car, Guid> carRepository = new BaseRepository<Car, Guid>(context);

            Car car = new Car
            {
                Id = Guid.NewGuid(),
                Brand = "Toyota",
                Model = "Corolla",
                RegistrationNumber = "В7122ТХ",
                IsHired = false,
                ImageUrl = "/photoPath"
            };

            context.Cars.Add(car);
            await context.SaveChangesAsync();

            CarService carService = new CarService(
                carRepository,
                _mapper,
                Mock.Of<IRepository<InsuranceBenefit, Guid>>(),
                Mock.Of<IRepository<Location, Guid>>(),
                Mock.Of<IRepository<Category, Guid>>(),
                Mock.Of<IRepository<Feature, Guid>>(),
                Mock.Of<IFileService>()
            );

            Result result = await carService.SetCarAsHired(car.Id.ToString());

            Assert.IsTrue(result.Success);
            Assert.AreEqual(CarStatusWasChangedToHired, result.Message);

            Car? updatedCar = await context.Cars.FirstOrDefaultAsync(c => c.Id == car.Id);
            Assert.IsNotNull(updatedCar);
            Assert.IsTrue(updatedCar.IsHired);
        }

        [Test]
        public async Task SetCarAsHired_ShouldReturnError_WhenGuidIsInvalid()
        {
            using RentACarDbContext context = new RentACarDbContext(_options);

            BaseRepository<Car, Guid> carRepository = new BaseRepository<Car, Guid>(context);

            CarService carService = new CarService(
                carRepository,
                _mapper,
                Mock.Of<IRepository<InsuranceBenefit, Guid>>(),
                Mock.Of<IRepository<Location, Guid>>(),
                Mock.Of<IRepository<Category, Guid>>(),
                Mock.Of<IRepository<Feature, Guid>>(),
                Mock.Of<IFileService>()
            );

            Result result = await carService.SetCarAsHired("invalid-guid");

            Assert.IsFalse(result.Success);
            Assert.AreEqual(InvalidGuidId, result.Message);
        }

        [Test]
        public async Task SetCarAsHired_ShouldReturnError_WhenCarDoesNotExist()
        {
            using RentACarDbContext context = new RentACarDbContext(_options);

            BaseRepository<Car, Guid> carRepository = new BaseRepository<Car, Guid>(context);

            CarService carService = new CarService(
                carRepository,
                _mapper,
                Mock.Of<IRepository<InsuranceBenefit, Guid>>(),
                Mock.Of<IRepository<Location, Guid>>(),
                Mock.Of<IRepository<Category, Guid>>(),
                Mock.Of<IRepository<Feature, Guid>>(),
                Mock.Of<IFileService>()
            );

            Guid nonExistentCarId = Guid.NewGuid();

            Result result = await carService.SetCarAsHired(nonExistentCarId.ToString());

            Assert.IsFalse(result.Success);
            Assert.AreEqual(InvalidCarId, result.Message);
        }

        [Test]
        public async Task SetCarAsHired_ShouldReturnError_WhenSaveChangesFails()
        {
            using RentACarDbContext context = new RentACarDbContext(_options);

            Mock<IRepository<Car, Guid>> carRepositoryMock = new Mock<IRepository<Car, Guid>>();
            Car car = new Car
            {
                Id = Guid.NewGuid(),
                Brand = "Toyota",
                IsHired = false
            };

            carRepositoryMock.Setup(repo => repo.GetByIdAsync(car.Id))
                .ReturnsAsync(car);

            carRepositoryMock.Setup(repo => repo.SaveChangesAsync())
                .ThrowsAsync(new Exception("Database error"));

            CarService carService = new CarService(
                carRepositoryMock.Object,
                _mapper,
                Mock.Of<IRepository<InsuranceBenefit, Guid>>(),
                Mock.Of<IRepository<Location, Guid>>(),
                Mock.Of<IRepository<Category, Guid>>(),
                Mock.Of<IRepository<Feature, Guid>>(),
                Mock.Of<IFileService>()
            );

            Result result = await carService.SetCarAsHired(car.Id.ToString());

            Assert.IsFalse(result.Success);
            Assert.AreEqual(CarStatusChangingError, result.Message);
        }


        [Test]
        public async Task ReleaseCar_ShouldReturnSuccess_WhenCarExists()
        {
            
            using RentACarDbContext context = new RentACarDbContext(_options);

            BaseRepository<Car, Guid> carRepository = new BaseRepository<Car, Guid>(context);

            Car car = new Car
            {
                Id = Guid.NewGuid(),
                Brand = "Toyota",
                IsHired = true,
                ImageUrl = "/photoPath",
                Model = "Corolla",
                RegistrationNumber = "В7122ТХ",
            };

            context.Cars.Add(car);
            await context.SaveChangesAsync();

            CarService carService = new CarService(
                carRepository,
                _mapper,
                Mock.Of<IRepository<InsuranceBenefit, Guid>>(),
                Mock.Of<IRepository<Location, Guid>>(),
                Mock.Of<IRepository<Category, Guid>>(),
                Mock.Of<IRepository<Feature, Guid>>(),
                Mock.Of<IFileService>()
            );

            
            Result result = await carService.ReleaseCar(car.Id.ToString());

            Assert.IsTrue(result.Success);
            Assert.AreEqual(CarStatusWasChangedТоReleased, result.Message);

            Car? updatedCar = await context.Cars.FirstOrDefaultAsync(c => c.Id == car.Id);
            Assert.IsNotNull(updatedCar);
            Assert.IsFalse(updatedCar.IsHired);
        }

        [Test]
        public async Task ReleaseCar_ShouldReturnError_WhenGuidIsInvalid()
        {
            using RentACarDbContext context = new RentACarDbContext(_options);

            BaseRepository<Car, Guid> carRepository = new BaseRepository<Car, Guid>(context);

            CarService carService = new CarService(
                carRepository,
                _mapper,
                Mock.Of<IRepository<InsuranceBenefit, Guid>>(),
                Mock.Of<IRepository<Location, Guid>>(),
                Mock.Of<IRepository<Category, Guid>>(),
                Mock.Of<IRepository<Feature, Guid>>(),
                Mock.Of<IFileService>()
            );

            Result result = await carService.ReleaseCar("invalid-guid");

            Assert.IsFalse(result.Success);
            Assert.AreEqual(InvalidGuidId, result.Message);
        }

        [Test]
        public async Task ReleaseCar_ShouldReturnError_WhenCarDoesNotExist()
        {
            using RentACarDbContext context = new RentACarDbContext(_options);

            BaseRepository<Car, Guid> carRepository = new BaseRepository<Car, Guid>(context);

            CarService carService = new CarService(
                carRepository,
                _mapper,
                Mock.Of<IRepository<InsuranceBenefit, Guid>>(),
                Mock.Of<IRepository<Location, Guid>>(),
                Mock.Of<IRepository<Category, Guid>>(),
                Mock.Of<IRepository<Feature, Guid>>(),
                Mock.Of<IFileService>()
            );

            Guid nonExistentCarId = Guid.NewGuid();

            Result result = await carService.ReleaseCar(nonExistentCarId.ToString());

            Assert.IsFalse(result.Success);
            Assert.AreEqual(InvalidCarId, result.Message);
        }

        [Test]
        public async Task ReleaseCar_ShouldReturnError_WhenSaveChangesFails()
        {
            using RentACarDbContext context = new RentACarDbContext(_options);

            Mock<IRepository<Car, Guid>> carRepositoryMock = new Mock<IRepository<Car, Guid>>();
            Car car = new Car
            {
                Id = Guid.NewGuid(),
                Brand = "Toyota",
                IsHired = true
            };

            carRepositoryMock.Setup(repo => repo.GetByIdAsync(car.Id))
                .ReturnsAsync(car);

            carRepositoryMock.Setup(repo => repo.SaveChangesAsync())
                .ThrowsAsync(new Exception("Database error"));

            CarService carService = new CarService(
                carRepositoryMock.Object,
                _mapper,
                Mock.Of<IRepository<InsuranceBenefit, Guid>>(),
                Mock.Of<IRepository<Location, Guid>>(),
                Mock.Of<IRepository<Category, Guid>>(),
                Mock.Of<IRepository<Feature, Guid>>(),
                Mock.Of<IFileService>()
            );

            Result result = await carService.ReleaseCar(car.Id.ToString());

            Assert.IsFalse(result.Success);
            Assert.AreEqual(CarStatusChangingError, result.Message);
        }
    }

}
