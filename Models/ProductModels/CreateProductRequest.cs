using InventoryManagement.Commons.Enums;

namespace InventoryManagement.Models.MerchandiseModels
{
    public class CreateProductRequest
    {
        public string CategoryId { get; set; }
        public string WarehouseId { get; set; }

        public string Name { get; set; }
        public float Price { get; set; }
        public string Unit { get; set; }
        public float Quantity { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
        public ActiveEnum IsActive { get; set; } = ActiveEnum.Active;
    }
}
