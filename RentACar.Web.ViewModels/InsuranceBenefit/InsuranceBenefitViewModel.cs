namespace RentACar.Web.ViewModels.InsuranceBenefit
{
    public class InsuranceBenefitViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public string IconClass { get; set; } = null!;

        public bool IsChecked { get; set; } = false;
    }
}
