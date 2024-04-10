using InventoryManagement.Commons.Enums;

namespace InventoryManagement.Models.SaleInvoiceModels
{
    public class SaleInvoiceViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CustomerId { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }
        public InvoiceStatusEnum Status { get; set; }
        public string? Note { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
