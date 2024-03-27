using InventoryManagement.Models.CategoryModels;
using InventoryManagement.Models.CommonModels;

namespace InventoryManagement.Services.Contractors
{
    public interface ICategoryService
    {
        public Task<ServiceResponseModel<CategoryViewModel>> Get(string id);
        public Task<ServiceResponseModel<List<CategoryViewModel>>> All();
        public Task<ServiceResponseModel<bool>> Create(CreateCategoryRequest request);
        public Task<ServiceResponseModel<bool>> Update(UpdateCategoryRequest request);
        public Task<ServiceResponseModel<bool>> Delete(string id);
    }
}
