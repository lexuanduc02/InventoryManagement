using InventoryManagement.Models.PurchaseInvoiceModels;
using InventoryManagement.Models.SaleInvoiceModels;

namespace InventoryManagement.Models.PartnerModels
{
    public class PartnerDetailViewModel
    {
        public PartnerViewModel Partner { get; set; }
        public List<PurchaseInvoiceViewModel> PurchaseInvoices { get; set; }
        public List<SaleInvoiceViewModel> ReturnInvoices { get; set; }
    }
}
