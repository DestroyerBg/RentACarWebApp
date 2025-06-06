﻿using Microsoft.AspNetCore.Http;
using RentACar.Core.Interfaces;
using static RentACar.Common.Constants.DatabaseModelsConstants.Car;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
namespace RentACar.Core.Services
{
    public class FileService : IFileService
    {
        public async Task<string> SavePhotoToServerAsync(IFormFile file, string photoName)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            if (file.Length > MaxPhotoFileSize)
            {
                return null;
            }

            string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

            string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return null;
            }

            string currDate = DateTime.Now.ToString(UniqueDateFormat);

            string uploadDir = Path.Combine("wwwroot", "images", "cars");

            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            string uploadPath = Path.Combine(uploadDir, $"{photoName}{fileExtension}");

            using (FileStream stream = new FileStream(uploadPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"~/images/cars/{photoName}{fileExtension}"; 
        }

        public async Task<string> ChangePhotoAsync(IFormFile file, string imageUrl, string photoName)
        {
            string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            string uploadDir = Path.Combine(wwwRootPath, "images", "cars");

           
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            imageUrl = imageUrl?.TrimStart('~').TrimStart('/');

            string physicalPath = Path.Combine(wwwRootPath, imageUrl?.Replace('/', Path.DirectorySeparatorChar));

            if (File.Exists(physicalPath))
            {
                try
                {
                    File.Delete(physicalPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"File not found: {physicalPath}");
            }

            string newPhotoPath = await SavePhotoToServerAsync(file, photoName);

            return newPhotoPath;
        }
    }
}

