using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentACar.Core.Interfaces;
using RentACar.Data.Models;
using RentACar.Data.Repository.Interfaces;
using RentACar.DTO.Car;
using RentACar.DTO.CustomerFeedback;

namespace RentACar.Core.Services
{
    public class CustomerFeedbackService : ICustomerFeedbackService
    {
        private readonly IRepository<CustomerFeedback, Guid> customerFeedbackRepository;
        private readonly IRepository<Car, Guid> carRepository;
        private readonly IMapper mapperService;
        public CustomerFeedbackService(IRepository<CustomerFeedback, Guid> _customerFeedbackRepository,
            IRepository<Car, Guid> _carRepository,
            IMapper _mapperService)
        {
            customerFeedbackRepository = _customerFeedbackRepository;
            carRepository = _carRepository;
            mapperService = _mapperService;
        }
        public async Task<SendFeedbackDTO> CreateSendFeedbackDTO()
        {
            SendFeedbackDTO dto = new SendFeedbackDTO();

            dto.Cars = await carRepository
                .GetAllAttached()
                .Select(c => mapperService.Map<FeedbackCarDTO>(c))
                .ToListAsync();

            return dto;
        }
    }
}
