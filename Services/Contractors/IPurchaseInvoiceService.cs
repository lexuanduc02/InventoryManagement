using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.PurchaseInvoiceModels;

namespace InventoryManagement.Services.Contractors
{
    public interface IPurchaseInvoiceService
    {
        public Task<ServiceResponseModel<PurchaseInvoiceViewModel>> GetAsync(string id);
        public Task<ServiceResponseModel<List<PurchaseInvoiceViewModel>>> AllAsync();
        public Task<ServiceResponseModel<string>> CreateAsync(CreatePurchaseInvoiceRequest request);
        public Task<ServiceResponseModel<bool>> DeleteAsync(string id);
        public Task<ServiceResponseModel<string>> UpdateAsync(UpdatePurchaseInvoiceRequest request);
    }
}
