using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.ImageModels;
using InventoryManagement.Models.Options;
using InventoryManagement.Services.Contractors;
using Microsoft.Extensions.Options;

namespace InventoryManagement.Services
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;

        public ImageService(IOptions<CloudinaryOption> options)
        {
            var acc = new Account()
            {
                Cloud = options.Value.CloudName,
                ApiKey = options.Value.ApiKey,
                ApiSecret = options.Value.ApiSecret,
            };

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ServiceResponseModel<ImageUploadResult>> AddImageAsync(UploadImageModel model)
        {
            var response = new ServiceResponseModel<ImageUploadResult>()
            {
                isSuccess = false,
            };

            var uploadResult = new ImageUploadResult();

            try
            {
                if (model.File.Length > 0)
                {
                    using var stream = model.File.OpenReadStream();
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(model.FileName, stream),
                        Transformation = new Transformation().Height(model.Height).Width(model.With).Crop("fill").Gravity("face"),
                        DisplayName = model.FileName,
                        UseFilename = true,
                        UniqueFilename = false,
                        Folder = model.Folder,
                    };

                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }

                response.isSuccess = true;
                response.data = uploadResult;

                return response;
            }
            catch (System.Exception ex)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<bool>> DeleteImageAsync(string name, string folder)
        {
            var response = new ServiceResponseModel<bool>()
            {
                isSuccess = false,  
            };

            try
            {
                var publicId = $"{folder}/{name}";

                var deleteParams = new DeletionParams(publicId);

                var result = await _cloudinary.DestroyAsync(deleteParams);

                if (result.Error == null)
                    response.isSuccess = true;

                return response;
            }
            catch (System.Exception ex)
            {
                return response;
            }
        }
    }
}
