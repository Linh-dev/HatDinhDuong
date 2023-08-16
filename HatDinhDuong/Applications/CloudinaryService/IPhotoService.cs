using CloudinaryDotNet.Actions;

namespace HatDinhDuong.Applications.CloudinaryService
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
