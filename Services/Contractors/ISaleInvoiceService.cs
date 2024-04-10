using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.SaleInvoiceModels;

namespace InventoryManagement.Services.Contractors
{
    public interface ISaleInvoiceService
    {
        public Task<ServiceResponseModel<bool>> CreateAsync(CreateSaleInvoiceRequest request);
    }
}
