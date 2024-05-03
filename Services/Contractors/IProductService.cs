using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.MerchandiseModels;
using InventoryManagement.Models.ProductModels;

namespace InventoryManagement.Services.Contractors
{
    public interface IProductService
    {
        public Task<ServiceResponseModel<ProductViewModel>> Get(string id);
        public Task<ServiceResponseModel<List<ProductViewModel>>> All();
        public Task<ServiceResponseModel<bool>> Create(CreateProductRequest request);
        public Task<ServiceResponseModel<bool>> Update(UpdateProductRequest request);
        public Task<ServiceResponseModel<bool>> Delete(string id);
        public Task<ServiceResponseModel<bool>> UpdateQuantityAsync(List<UpdateProductQuantityRequest> requests);
    }
}
