using InventoryManagement.Commons.Enums;

namespace InventoryManagement.Models.MerchandisePurchaseModels
{
    public class UpdateMerchandisePurchaseRequest
    {
        public Guid PurchaseInvoiceId { get; set; }
        public Guid MerchandiseId { get; set; }
        public ActiveEnum IsActive { get; set; } = ActiveEnum.Active;
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string? Unit { get; set; }
        public float PurchasePrice { get; set; }
    }
}
