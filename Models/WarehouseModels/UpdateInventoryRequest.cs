using InventoryManagement.Models.MerchandisePurchaseModels;
using InventoryManagement.Models.PurchaseInvoiceModels;

namespace InventoryManagement.Models.WarehouseModels
{
    public class UpdateInventoryRequest
    {
        public PurchaseInvoiceViewModel PurchaseInvoiceViewModel { get; set; }
        public List<MerchandisePurchaseViewModel> MerchandisePurchaseViewModels { get; set; }
    }
}
