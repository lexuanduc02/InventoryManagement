using InventoryManagement.Commons.Enums;

namespace InventoryManagement.Models.CategoryModels
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
        public ActiveEnum IsActive = ActiveEnum.Active;
    }
}
