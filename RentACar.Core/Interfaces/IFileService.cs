using Microsoft.AspNetCore.Http;

namespace RentACar.Core.Interfaces
{
    public interface IFileService
    {
        Task<string> SavePhotoToServerAsync(IFormFile file, string photoName);

        Task<string> ChangePhotoAsync(IFormFile file, string imageUrl, string photoName);
    }
}
