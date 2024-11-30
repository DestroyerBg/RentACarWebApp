using Microsoft.AspNetCore.Http;

namespace RentACar.Core.Interfaces
{
    public interface IFileService
    {
        Task<bool> SavePhotoToServerAsync(IFormFile file, string photoName);
    }
}
