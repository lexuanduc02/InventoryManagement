using InventoryManagement.Commons.Enums;

namespace InventoryManagement.Views.SaleInvoice
{
    public class SaleInvoiceViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string CustomerName { get; set; }
        public InvoiceStatusEnum Status { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public float Total { get; set; }
    }
}
