using InventoryManagement.Models.MerchandiseSaleInvoiceModels;

namespace InventoryManagement.Models.SaleInvoiceModels
{
    public class DetailSaleInvoiceViewModel 
    {
        public SaleInvoiceViewModel SaleInvoiceViewModel { get; set; }
        public List<MerchandiseSaleInvoiceViewModel> MerchandiseSaleInvoiceViewModels { get; set; }
    }
}
