namespace RentACar.Web.ViewModels.Reservation
{
    public class ReservationDoneViewModel
    {
        public string CarBrand { get; set; } = null!;
        public string CarModel { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Address { get; set; } = null!;
    }
}
