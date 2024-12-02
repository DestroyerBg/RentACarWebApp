using Microsoft.AspNetCore.Http;
using RentACar.Core.Interfaces;
using static RentACar.Common.Constants.DatabaseModelsConstants.Car;
namespace RentACar.Core.Services
{
    public class FileService : IFileService
    {
        public async Task<string> SavePhotoToServerAsync(IFormFile file)
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

            string currDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            string photoName = $"{currDate}{fileExtension}";

            string uploadDir = Path.Combine("wwwroot", "images", "cars");

            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            string uploadPath = Path.Combine(uploadDir, photoName);

            using (FileStream stream = new FileStream(uploadPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/images/cars/{photoName}"; 
        }

    }
}

