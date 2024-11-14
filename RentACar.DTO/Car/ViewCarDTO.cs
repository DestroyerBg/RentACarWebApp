using static RentACar.Common.Constants.DatabaseModelsConstants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentACar.DTO.Car
{
    public class ViewCarDTO
    {
        public Guid Id { get; set; }

        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int HorsePower { get; set; }

        public string RegistrationNumber { get; set; } = null!;

        public int YearOfManufacture { get; set; }

        public Guid LocationId { get; set; }

        public string ImageUrl { get; set; } = null!;

    }
}
