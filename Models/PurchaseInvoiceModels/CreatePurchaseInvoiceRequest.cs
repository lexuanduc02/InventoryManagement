using InventoryManagement.Commons.Enums;
using InventoryManagement.Models.MerchandisePurchaseModels;

namespace InventoryManagement.Models.PurchaseInvoiceModels
{
    public class CreatePurchaseInvoiceRequest
    {
        public string UserId { get; set; }
        public string PartnerId { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }
        public InvoiceStatusEnum Status { get; set; } = InvoiceStatusEnum.InProcess;
        public string? Note { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public ActiveEnum IsActive { get; set; } = ActiveEnum.Active;
        public List<CreateMerchandisePurchaseRequest> MerchandisePurchaseInvoices { get; set; }
    }
}
