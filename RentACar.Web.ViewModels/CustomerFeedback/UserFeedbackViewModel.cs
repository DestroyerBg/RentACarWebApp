namespace RentACar.Web.ViewModels.CustomerFeedback
{
    public class UserFeedbackViewModel
    {
        public string Id { get; set; } = null!;
        public string CarBrandAndModel { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CustomerUsername { get; set; } = null!;
        public int Stars { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
