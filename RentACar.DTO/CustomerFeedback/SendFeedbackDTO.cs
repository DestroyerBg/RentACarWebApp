using RentACar.DTO.Car;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
namespace RentACar.DTO.CustomerFeedback
{
    public class SendFeedbackDTO
    {
        public SendFeedbackDTO()
        {
            DateOfSubmission = DateTime.Now.ToString(DateFormat);
        }
        
        public ICollection<FeedbackCarDTO> Cars { get; set; } = new HashSet<FeedbackCarDTO>();

        public string CustomerId { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string CarId { get; set; } = null!;

        public string? DateOfSubmission { get; set; }

        public int? Rating { get; set; } 
    }
}
