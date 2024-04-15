using InventoryManagement.Models.MerchandisePurchaseModels;

namespace InventoryManagement.Models.PurchaseInvoiceModels
{
    public class DetailPurchaseInvoiceModel
    {
        public PurchaseInvoiceViewModel PurchaseInvoiceViewModel { get; set; }
        public List<MerchandisePurchaseViewModel> MerchandisePurchaseViewModels { get; set; }
    }
}
