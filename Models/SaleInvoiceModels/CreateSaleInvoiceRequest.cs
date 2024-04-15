using InventoryManagement.Commons.Enums;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Models.CustomerModels;
using System.ComponentModel;

namespace InventoryManagement.Models.SaleInvoiceModels
{
    public class CreateSaleInvoiceRequest
    {
        public string UserId { get; set; }  
        public string? CustomerId { get; set;}

        public CreateCustomerRequest Customer { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }
        public InvoiceStatusEnum Status { get; set; } = InvoiceStatusEnum.InProcess;
        public string? Note { get; set; }
        public string? ShippingCarrier { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public ActiveEnum IsActive { get; set; } = ActiveEnum.Active;
        public List<MerchandiseSale> MerchandiseSaleInvoices { get; set; }
    }

    public class MerchandiseSale
    {
        public Guid? SaleInvoiceId { get; set; }

        public Guid MerchandiseId { get; set; }

        public string? Name { get; set; }

        public int Quantity { get; set; }
        public string? Unit { get; set; }
        public float SellingPrice { get; set; }
        public float Voucher { get; set; }

        [DefaultValue(ActiveEnum.Active)]
        public ActiveEnum IsActive { get; set; } = ActiveEnum.Active;
    }
}
