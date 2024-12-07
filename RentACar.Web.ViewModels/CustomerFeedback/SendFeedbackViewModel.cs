using RentACar.DTO.Car;
using RentACar.Web.ViewModels.Car;

namespace RentACar.Web.ViewModels.CustomerFeedback
{
    public class SendFeedbackViewModel
    {
        public ICollection<FeedbackCarViewModel> Cars { get; set; } = new HashSet<FeedbackCarViewModel>();

        public string CustomerId { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string CarId { get; set; } = null!;

        public string? DateOfSubmission { get; set; }
    }
}
