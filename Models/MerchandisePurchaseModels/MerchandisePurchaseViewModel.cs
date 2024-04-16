using InventoryManagement.Commons.Enums;

namespace InventoryManagement.Models.MerchandisePurchaseModels
{
    public class MerchandisePurchaseViewModel
    {
        public string PurchaseInvoiceId { get; set; }
        public string MerchandiseId { get; set; }
        public ActiveEnum IsActive { get; set; }
        public float MerchandisePrice { get; set; }
        public int Quantity { get; set; }
        public string? Unit { get; set; }
        public float PurchasePrice { get; set; }
    }
}
