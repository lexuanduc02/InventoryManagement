using InventoryManagement.Commons.Enums;

namespace InventoryManagement.Models.CategoryModels
{
    public class CreateCategoryRequest
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }

        public ActiveEnum IsActive = ActiveEnum.Active;
    }
}
