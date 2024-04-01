using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.CustomerModels;

namespace InventoryManagement.Services.Contractors
{
    public interface ICustomerService
    {
        public Task<ServiceResponseModel<List<CustomerViewModel>>> All();
        public Task<ServiceResponseModel<CustomerViewModel>> GetAsync(string id);
        public Task<ServiceResponseModel<bool>> CreateAsync(CreateCustomerRequest request);
        public Task<ServiceResponseModel<bool>> UpdateAsync(UpdateCustomerRequest request);
        public Task<ServiceResponseModel<bool>> DeleteAsync(string id);
    }
}
