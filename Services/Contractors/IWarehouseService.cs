using InventoryManagement.Models.WarehouseModels;

namespace InventoryManagement.Services.Contractors
{
    public interface IWarehouseService
    {
        public bool Create(CreateWarehouseRequest request);
    }
}
