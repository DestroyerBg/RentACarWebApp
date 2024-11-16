﻿using RentACar.DTO.Car;

namespace RentACar.Core.Interfaces
{
    public interface ICarService
    { 
        Task<IEnumerable<ViewCarDTO>> GetCarsAsync();
    }
}