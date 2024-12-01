using Microsoft.AspNetCore.Http;

namespace RentACar.Core.Interfaces
{
    public interface IFileService
    {
        Task<string> SavePhotoToServerAsync(IFormFile file);
    }
}
