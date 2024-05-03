using InventoryManagement.Commons.Enums;

namespace InventoryManagement.Models.WarehouseModels
{
    public class WarehouseViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public float StorageCapacity { get; set; }
        public float? Area { get; set; }
        public string Unit { get; set; }
        public string? Description { get; set; }
        public WarehouseCapacityEnum WarehouseCapacity { get; set; }

        public string WarehouseCapacityString {
            get {
                if (WarehouseCapacity == WarehouseCapacityEnum.Empty)
                    return "Còn trống";
                else
                    return "Đã đầy";
            }
        }
    }
}
