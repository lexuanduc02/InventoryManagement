using InventoryManagement.Commons.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Domains.Entities
{
    [Table("SaleInvoices")]
    public class SaleInvoice
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public PaymentMethodEnum PaymentMethod { get; set; }
        public InvoiceStatusEnum Status { get; set; }
        public string? Note { get; set; }
        public string? ShippingCarrier { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public ActiveEnum IsActive { get; set; } = ActiveEnum.Active;
        public InvoiceTypeEnum InvoiceTypeEnum { get; set; } = InvoiceTypeEnum.Invoice;

        public ICollection<MerchandiseSaleInvoice> MerchandiseSaleInvoices { get; set; }
    }
}
