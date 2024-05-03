using InventoryManagement.Models.MerchandiseSaleInvoiceModels;

namespace InventoryManagement.Models.SaleInvoiceModels
{
    public class UpdateSaleInvoiceRequest
    {
        public SaleInvoiceViewModel SaleInvoiceViewModel { get; set; }
        public List<MerchandiseSaleInvoiceViewModel>? MerchandiseSaleInvoiceViewModels { get; set; }
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}
