﻿using System.ComponentModel.DataAnnotations;
using RentACar.DTO.Identity;
using RentACar.Services.Infrastructure.AutoMapper;

namespace RentACar.Web.ViewModels.Identity
{
    public class LoginViewModel : IMapTo<LoginDTO>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
