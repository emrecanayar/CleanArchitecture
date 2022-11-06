using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace rentACar.Infrastructure.Adapters.ImageService
{
    public class CloudinaryImageServiceAdapter : IImageService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryImageServiceAdapter(IConfiguration configuration)
        {
            Account account = configuration.GetSection("CloudinaryAccount").Get<Account>();
            _cloudinary = new Cloudinary(account);
        }

        public void Delete(string imageUrl)
        {
            DeletionParams deletionParams = new(getPublicId(imageUrl));
            _cloudinary.Destroy(deletionParams);
        }

        public string Update(IFormFile formFile, string imageUrl)
        {
            Delete(imageUrl);
            return Upload(formFile);
        }

        public string Upload(IFormFile formFile)
        {
            ImageUploadParams imageUploadParams = new()
            {
                File = new(formFile.FileName, formFile.OpenReadStream()),
                UseFilename = false,
                UniqueFilename = true,
                Overwrite = false
            };
            ImageUploadResult imageUploadResult = _cloudinary.Upload(imageUploadParams);

            return imageUploadResult.Url.ToString();
        }

        private string getPublicId(string imageUrl)
        {
            int startIndex = imageUrl.LastIndexOf('/') + 1;
            int endIndex = imageUrl.LastIndexOf('.');
            int length = endIndex - startIndex;
            return imageUrl.Substring(startIndex, length);
        }
    }
}
