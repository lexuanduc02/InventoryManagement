using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.WarehouseModels;

namespace InventoryManagement.Services.Contractors
{
    public interface IWarehouseService
    {
        public Task<ServiceResponseModel<WarehouseViewModel>> Get(string id);
        public Task<ServiceResponseModel<List<WarehouseViewModel>>> All();
        public Task<ServiceResponseModel<bool>> Create(CreateWarehouseRequest request);
        public Task<ServiceResponseModel<bool>> Update(UpdateWarehouseRequest request);
        public Task<ServiceResponseModel<string>> UpdateInventoryAsync(UpdateInventoryRequest request);
        public Task<ServiceResponseModel<bool>> Delete(string id);
    }
}
