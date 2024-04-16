using InventoryManagement.Commons.Enums;
using InventoryManagement.Models.MerchandisePurchaseModels;

namespace InventoryManagement.Models.PurchaseInvoiceModels
{
    public class UpdatePurchaseInvoiceRequest
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PartnerId { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }
        public InvoiceStatusEnum Status { get; set; } = InvoiceStatusEnum.InProcess;
        public string? Note { get; set; }
        public DateTime UpdateAt { get; set; } = DateTime.Now;
        public List<MerchandisePurchaseViewModel>? MerchandisePurchaseViewModels { get; set; }
    }
}
