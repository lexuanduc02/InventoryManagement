using InventoryManagement.Commons.Enums;

namespace InventoryManagement.Models.WarehouseModels
{
    public class UpdateWarehouseRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public float? Area { get; set; }
        public string? Description { get; set; }
        public WarehouseCapacityEnum WarehouseCapacity { get; set; }
    }
}
