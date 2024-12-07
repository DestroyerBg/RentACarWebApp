using RentACar.DTO.CustomerFeedback;

namespace RentACar.Core.Interfaces
{
    public interface ICustomerFeedbackService
    {
        Task<SendFeedbackDTO> CreateSendFeedbackDTO();
    }
}
