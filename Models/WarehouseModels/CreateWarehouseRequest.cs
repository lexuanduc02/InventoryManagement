using InventoryManagement.Commons.Enums;

namespace InventoryManagement.Models.WarehouseModels
{
    public class CreateWarehouseRequest
    {
        public string Name { get; set; }
        public float? Area { get; set; }
        public string? Description { get; set; }

        public ActiveEnum IsActive = ActiveEnum.Active;
    }
}
