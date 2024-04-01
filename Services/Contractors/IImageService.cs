using CloudinaryDotNet.Actions;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.ImageModels;

namespace InventoryManagement.Services.Contractors
{
    public interface IImageService
    {
        Task<ServiceResponseModel<ImageUploadResult>> AddImageAsync(UploadImageModel model);
        Task<ServiceResponseModel<bool>> DeleteImageAsync(string name, string folder);
    }
}
