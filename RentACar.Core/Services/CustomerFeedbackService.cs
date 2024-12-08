using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentACar.Core.Interfaces;
using RentACar.Data.Models;
using RentACar.Data.Repository.Interfaces;
using RentACar.DTO.Car;
using RentACar.DTO.CustomerFeedback;
using RentACar.DTO.Result;
using static RentACar.Common.Constants.DatabaseModelsConstants.CustomerFeedback;
using static RentACar.Common.Messages.DatabaseModelsMessages.Common;
using static RentACar.Common.Messages.DatabaseModelsMessages.CustomerFeedback;
namespace RentACar.Core.Services
{
    public class CustomerFeedbackService :BaseService, ICustomerFeedbackService
    {
        private readonly IRepository<CustomerFeedback, Guid> customerFeedbackRepository;
        private UserManager<ApplicationUser> userManager;
        private readonly IRepository<Car, Guid> carRepository;
        private readonly IMapper mapperService;
        public CustomerFeedbackService(IRepository<CustomerFeedback, Guid> _customerFeedbackRepository,
            UserManager<ApplicationUser> _userManager,
            IRepository<Car, Guid> _carRepository,
            IMapper _mapperService)
        {
            customerFeedbackRepository = _customerFeedbackRepository;
            carRepository = _carRepository;
            mapperService = _mapperService;
            userManager = _userManager;
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

        public async Task<Result> CreateFeedback(SendFeedbackDTO dto, ClaimsPrincipal claim)
        {
            if (dto.Rating == null || (dto.Rating < RatingMinValue || dto.Rating > RatingMaxValue))
            {
                return new Result()
                {
                    Message = RatingValuesError,
                    Success = false
                };
            }

            ApplicationUser? user = await userManager.GetUserAsync(claim);

            if (user == null)
            {
                return new Result()
                {
                    Message = OnlyLoggedInUsersCanSendFeedback,
                    Success = false
                };
            }

            CustomerFeedback customerFeedback = mapperService.Map<CustomerFeedback>(dto);
            customerFeedback.CustomerId = user.Id;
            try
            {
                await customerFeedbackRepository.AddAsync(customerFeedback);
                await customerFeedbackRepository.SaveChangesAsync();

                return new Result() { Success = true };
            }
            catch (Exception e)
            {
                return new Result()
                {
                    Message = ErrorWhenCreatingCustomerFeedback,
                    Success = false
                };
            }
        }

        public async Task<IEnumerable<UserFeedbackDTO>> GetAllFeedbacks()
        {
            IEnumerable<UserFeedbackDTO> dtos = await customerFeedbackRepository
                .GetAllAttached()
                .Include(c => c.Car)
                .Include(c => c.Customer)
                .Select(c => mapperService.Map<UserFeedbackDTO>(c))
                .ToListAsync();

            return dtos;
        }

        public async Task<Result> DeleteFeedback(string id)
        {
            if (!base.IsValidGuid(id))
            {
                return new Result()
                {
                    Message = InvalidGuidId,
                    Success = false
                };
            }

            CustomerFeedback? customerFeedback = await customerFeedbackRepository.GetByIdAsync(Guid.Parse(id));

            if (customerFeedback == null)
            {
                return new Result()
                {
                    Message = InvalidCustomerFeedbackId,
                    Success = false
                };
            }

            try
            {
                await customerFeedbackRepository.DeleteAsync(customerFeedback);
                await customerFeedbackRepository.SaveChangesAsync();

                return new Result()
                {
                    Success = true,
                    Message = SuccessWithDeletingFeedback
                };
            }
            catch (Exception e)
            {
                return new Result()
                {
                    Success = false,
                    Message = ErrorWithDeletingTheFeedback,
                };
            }
        }
    }
}
