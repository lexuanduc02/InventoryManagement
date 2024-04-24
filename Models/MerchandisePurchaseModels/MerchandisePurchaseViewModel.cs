using InventoryManagement.Commons.Enums;
using InventoryManagement.Commons.Extensions;

namespace InventoryManagement.Models.MerchandisePurchaseModels
{
    public class MerchandisePurchaseViewModel
    {
        public string PurchaseInvoiceId { get; set; }
        public string MerchandiseId { get; set; }
        public string MerchandiseName { get; set; }
        public ActiveEnum IsActive { get; set; }
        public float MerchandisePrice { get; set; }
        public int Quantity { get; set; }
        public string? Unit { get; set; }
        public float PurchasePrice { get; set; }

        public string PurchasePriceToVND
        {
            get
            {
                return PurchasePrice.FormatVietnameseCurrency();
            }
        }

        public string SubTotalToVND
        {
            get
            {
                var total = Quantity * PurchasePrice;
                return total.FormatVietnameseCurrency();
            }
        }
    }
}
