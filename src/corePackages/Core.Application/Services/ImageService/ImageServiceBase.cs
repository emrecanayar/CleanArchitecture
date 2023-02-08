using Core.CrossCuttingConcerns.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Services.ImageService
{
    public abstract class ImageServiceBase
    {
        public abstract Task<string> UploadAsync(IFormFile formFile);

        public async Task<string> UpdateAsync(IFormFile formFile, string imageUrl)
        {
            await fileMustBeInImageFormat(formFile);

            await DeleteAsync(imageUrl);
            return await UploadAsync(formFile);
        }

        public abstract Task DeleteAsync(string imageUrl);

        protected async Task fileMustBeInImageFormat(IFormFile formFile)
        {
            List<string> extensions = new() { ".jpg", ".png", ".jpeg", ".webp" };

            string extension = Path.GetExtension(formFile.FileName).ToLower();
            if (!extensions.Contains(extension)) throw new BusinessException("Unsupported format");
            await Task.CompletedTask;
        }
    }
}