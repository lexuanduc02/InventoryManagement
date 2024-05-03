using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.MerchandisePurchaseModels;

namespace InventoryManagement.Services.Contractors
{
    public interface IMerchandisePurchaseInvoiceService
    {
        public Task<ServiceResponseModel<List<MerchandisePurchaseViewModel>>> GetByInvoiceAsync(string invoiceId);
    }
}
