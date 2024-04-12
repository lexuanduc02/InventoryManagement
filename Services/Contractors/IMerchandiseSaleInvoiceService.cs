using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.MerchandiseSaleInvoiceModels;

namespace InventoryManagement.Services.Contractors
{
    public interface IMerchandiseSaleInvoiceService
    {
        public Task<ServiceResponseModel<List<MerchandiseSaleInvoiceViewModel>>> GetByInvoiceAsync(string invoiceId);
    }
}
