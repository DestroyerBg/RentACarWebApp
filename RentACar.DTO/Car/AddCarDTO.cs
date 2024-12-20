﻿using RentACar.Common.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using RentACar.DTO.Category;
using RentACar.DTO.Feature;
using RentACar.DTO.Location;

namespace RentACar.DTO.Car
{
    public class AddCarDTO
    {
        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int YearOfManufacture { get; set; }

        public int HorsePower { get; set; }

        public string RegistrationNumber { get; set; } = null!;

        public string CategoryId { get; set; }

        public string LocationId { get; set; }

        public decimal PricePerDay { get; set; }

        public string? CarImageUrl { get; set; }

        public IFormFile? CarImage { get; set; } 

        public ICollection<LocationDTO> Locations { get; set; } = new HashSet<LocationDTO>();

        public ICollection<CategoryDTO> Categories { get; set; } = new HashSet<CategoryDTO>();

        public ICollection<FeatureCheckboxDTO> Features { get; set; } = new HashSet<FeatureCheckboxDTO>();
    }
}
