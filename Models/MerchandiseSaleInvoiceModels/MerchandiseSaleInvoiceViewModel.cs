using InventoryManagement.Commons.Enums;
using InventoryManagement.Commons.Extensions;

namespace InventoryManagement.Models.MerchandiseSaleInvoiceModels
{
    public class MerchandiseSaleInvoiceViewModel
    {
        public string SaleInvoiceId { get; set; }
        public string MerchandiseId { get; set; }
        public string MerchandiseName { get; set; }
        public int Quantity { get; set; }
        public float SellingPrice { get; set; }
        public float Voucher { get; set; }
        public ActiveEnum IsActive { get; set; } = ActiveEnum.Active;

        public string SellingPriceToVND
        {
            get
            {
                return SellingPrice.FormatVietnameseCurrency();
            }
        }

        public string SubTotalToVND
        {
            get
            {
                var total = Quantity * SellingPrice * (100 - Voucher) / 100;
                return total.FormatVietnameseCurrency();
            }
        }
    }
}
