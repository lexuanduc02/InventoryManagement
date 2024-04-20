using InventoryManagement.Commons.Enums;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.PurchaseInvoiceModels;

namespace InventoryManagement.Services.Contractors
{
    public interface IPurchaseInvoiceService
    {
        public Task<ServiceResponseModel<PurchaseInvoiceViewModel>> GetAsync(string id);
        public Task<ServiceResponseModel<List<PurchaseInvoiceViewModel>>> GetByPartnerIdAsync(string id, InvoiceTypeEnum invoiceType = InvoiceTypeEnum.Invoice);
        public Task<ServiceResponseModel<List<PurchaseInvoiceViewModel>>> AllAsync(InvoiceTypeEnum invoiceType);
        public Task<ServiceResponseModel<List<PurchaseInvoice>>> AllUnUpdateWarehouseInvoiceAsync();
        public Task<ServiceResponseModel<string>> CreateAsync(CreatePurchaseInvoiceRequest request);
        public Task<ServiceResponseModel<string>> CreateReturnAsync(CreatePurchaseInvoiceReturnRequest request);
        public Task<ServiceResponseModel<bool>> DeleteAsync(string id);
        public Task<ServiceResponseModel<string>> UpdateAsync(UpdatePurchaseInvoiceRequest request);
    }
}
