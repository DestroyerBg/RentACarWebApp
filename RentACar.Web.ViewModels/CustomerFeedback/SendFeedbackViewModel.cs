using System.ComponentModel.DataAnnotations;
using RentACar.Web.ViewModels.Car;
using static RentACar.Common.Messages.DatabaseModelsMessages.Common;
using static RentACar.Common.Messages.DatabaseModelsMessages.CustomerFeedback;
using static RentACar.Common.Constants.DatabaseModelsConstants.CustomerFeedback;
using RentACar.Common.ValidationAttributes;
namespace RentACar.Web.ViewModels.CustomerFeedback
{
    public class SendFeedbackViewModel
    {
        public ICollection<FeedbackCarViewModel> Cars { get; set; } = new HashSet<FeedbackCarViewModel>();

        public string? CustomerId { get; set; }

        [Required(ErrorMessage = FieldIsRequired)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        public string CarId { get; set; } = null!;

        public int? Stars { get; set; }
    }
}
