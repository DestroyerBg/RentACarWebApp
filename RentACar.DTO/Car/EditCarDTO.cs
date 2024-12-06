using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentACar.DTO.Category;
using RentACar.DTO.Feature;

namespace RentACar.DTO.Car
{
    public class EditCarDTO
    {
        public string Id { get; set; } = null!;
        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int YearOfManufacture { get; set; }

        public int HorsePower { get; set; }

        public string RegistrationNumber { get; set; } = null!;

        public decimal PricePerDay { get; set; }

        public string? CarImageUrl { get; set; }

        public IFormFile? CarImage { get; set; }

        public ICollection<SelectListItem> Locations { get; set; } = new HashSet<SelectListItem>();

        public ICollection<SelectListItem> Categories { get; set; } = new HashSet<SelectListItem>();

        public ICollection<SelectListItem> Features { get; set; } = new HashSet<SelectListItem>();
    }
}
