﻿using InventoryManagement.Commons.Enums;
using InventoryManagement.Domains.Contractors;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Domains.Entities
{
    [Table("MerchandisePurchaseInvoices")]
    public class MerchandisePurchaseInvoice : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid PurchaseInvoiceId { get; set; }
        public PurchaseInvoice PurchaseInvoice { get; set; }

        public Guid MerchandiseId { get; set; }
        public Merchandise Merchandise { get; set; }

        [DefaultValue(ActiveEnum.Active)]
        public ActiveEnum IsActive { get; set; }

        public float MerchandisePrice { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public float PurchasePrice { get; set; }
    }
}
