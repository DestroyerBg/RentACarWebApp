using RentACar.DTO.CustomerFeedback;
using RentACar.DTO.Result;
using System.Security.Claims;

namespace RentACar.Core.Interfaces
{
    public interface ICustomerFeedbackService
    {
        Task<SendFeedbackDTO> CreateSendFeedbackDTO();
        Task<Result> CreateFeedback(SendFeedbackDTO dto, ClaimsPrincipal claim);
    }
}
