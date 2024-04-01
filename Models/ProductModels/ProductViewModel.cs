using InventoryManagement.Commons.Enums;

namespace InventoryManagement.Models.MerchandiseModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string WarehouseId { get; set; }
        public string WarehouseName { get; set; }

        public string Name { get; set; }
        public float Price { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        public ActiveEnum IsActive { get; set; }
    }
}
