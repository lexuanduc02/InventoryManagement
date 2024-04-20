using InventoryManagement.Models.PurchaseInvoiceModels;

namespace InventoryManagement.Models.PartnerModels
{
    public class PartnerDetailViewModel
    {
        public PartnerViewModel Partner { get; set; }
        public List<PurchaseInvoiceViewModel> PurchaseInvoices { get; set; }
        public List<PurchaseInvoiceViewModel> ReturnInvoices { get; set; }
    }
}
