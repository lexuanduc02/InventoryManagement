﻿using InventoryManagement.Commons.Enums;
using System.ComponentModel;

namespace InventoryManagement.Models.MerchandiseSaleInvoiceModels
{
    public class MerchandiseSaleInvoiceViewModel
    {
        public string SaleInvoiceId { get; set; }

        public string MerchandiseId { get; set; }

        public int Quantity { get; set; }
        public float SellingPrice { get; set; }
        public float Voucher { get; set; }

        [DefaultValue(ActiveEnum.Active)]
        public ActiveEnum IsActive { get; set; } = ActiveEnum.Active;
    }
}