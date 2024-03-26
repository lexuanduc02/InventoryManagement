namespace InventoryManagement.Models.WarehouseModels
{
    public class CreateWarehouseRequest
    {
        public string Name { get; set; }
        public float StorageCapacity { get; set; }
        public float? Area { get; set; }
        public string Unit { get; set; }
        public string? Description { get; set; }
    }
}
