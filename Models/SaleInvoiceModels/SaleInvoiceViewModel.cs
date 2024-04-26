﻿using InventoryManagement.Commons.Enums;
using InventoryManagement.Commons.Extensions;

namespace InventoryManagement.Models.SaleInvoiceModels
{
    public class SaleInvoiceViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string? CustomerName { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerPhoneNumber { get; set; }

        public string? PartnerName { get; set; }
        public string? PartnerId { get; set; }

        public PaymentMethodEnum? PaymentMethod { get; set; }
        public InvoiceStatusEnum Status { get; set; }
        public InvoiceTypeEnum? InvoiceType { get; set; }
        public string? Note { get; set; }
        public string? ShippingCarrier { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public float Total { get; set; }

        public string TotalToVND
        {
            get
            {
                return Total.FormatVietnameseCurrency();
            }
        }

        public string CreateAtString
        {
            get
            {
                return CreateAt.ToDateOnly();
            }
        }

        public string UpdateAtString
        {
            get
            {
                return UpdateAt.ToDateOnly();
            }
        }

        public string VatTotal
        {
            get
            {
                var vatTotal = Total * 10 / 100;
                return vatTotal.FormatVietnameseCurrency();
            }
        }
    }
}
