namespace RentACar.Web.ViewModels.Admin
{
    public class ManageReservationViewModel
    {
        public string Id { get; set; } = null!;
        public string OrderNumber { get; set; } = null!;

        public string CarBrandAndModel { get; set; } = null!;

        public string AccountUsername { get; set; } = null!;
        public decimal TotalPrice { get; set; }
    }
}
