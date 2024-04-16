using InventoryManagement.Commons.Enums;

namespace InventoryManagement.Models.SaleInvoiceModels
{
    public class SaleInvoiceViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }
        public InvoiceStatusEnum Status { get; set; }
        public InvoiceTypeEnum InvoiceType { get; set; }
        public string? Note { get; set; }
        public string? ShippingCarrier { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public float Total { get; set; }
    }
}
