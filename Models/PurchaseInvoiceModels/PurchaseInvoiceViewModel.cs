using InventoryManagement.Commons.Enums;

namespace InventoryManagement.Models.PurchaseInvoiceModels
{
    public class PurchaseInvoiceViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PartnerId { get; set; }
        public string PartnerName { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }
        public InvoiceStatusEnum Status { get; set; }
        public string? Note { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public float Total { get; set; }
    }
}
