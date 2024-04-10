using InventoryManagement.Commons.Enums;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Models.CustomerModels;

namespace InventoryManagement.Models.SaleInvoiceModels
{
    public class CreateSaleInvoiceRequest
    {
        public string UserId { get; set; }  
        public string CustomerId { get; set;}

        public CreateCustomerRequest Customer { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }
        public InvoiceStatusEnum Status { get; set; } = InvoiceStatusEnum.InProcess;
        public string? Note { get; set; }
        public string? ShippingCarrier { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public ActiveEnum IsActive { get; set; } = ActiveEnum.Active;
        public List<MerchandiseSaleInvoice> MerchandiseSaleInvoices { get; set; }
    }
}
