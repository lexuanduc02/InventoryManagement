using InventoryManagement.Models.PurchaseInvoiceModels;
using InventoryManagement.Models.SaleInvoiceModels;

namespace InventoryManagement.Models.CustomerModels
{
    public class CustomerDetailViewModel
    {
        public CustomerViewModel Customer { get; set; }
        public List<SaleInvoiceViewModel> Invoice { get; set; }
        public List<PurchaseInvoiceViewModel> ReturnInvoice { get; set; }
    }
}
