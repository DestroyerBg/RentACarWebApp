namespace RentACar.Web.ViewModels.Reservation
{
    public class ReservationViewModel
    {
        public string Id { get; set; } = null!;
        public string CarBrand { get; set; } = null!; 
        public string CarModel { get; set; } = null!;
        public string CarImageUrl { get; set; } = null!;
        public string StartDate { get; set; } = null!;
        public string EndDate { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!; 
        public string Address { get; set; } = null!; 
        public string OrderNumber { get; set; } = null!;

        public string TotalPrice { get; set; } = null!;
    }
}
