﻿using InventoryManagement.Commons.Enums;
using InventoryManagement.Domains.Contractors;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Domains.Entities
{
    [Table("PurchaseInvoices")]
    public class PurchaseInvoice : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid? PartnerId { get; set; }
        public Partner? Partner { get; set; }

        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public PaymentMethodEnum PaymentMethod { get; set; }
        public InvoiceStatusEnum Status { get; set; }
        public string? Note { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public ActiveEnum IsActive { get; set; } = ActiveEnum.Active;
        public InvoiceTypeEnum InvoiceType { get; set; } = InvoiceTypeEnum.Invoice;
        public WarehouseStatusEnum WarehouseStatus { get; set; } = WarehouseStatusEnum.NotUpdate;

        public ICollection<MerchandisePurchaseInvoice> MerchandisePurchaseInvoices { get; set; }
    }
}
