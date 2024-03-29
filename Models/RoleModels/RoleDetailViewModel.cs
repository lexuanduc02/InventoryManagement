using InventoryManagement.Commons.Enums;
using InventoryManagement.Models.UserModels;

namespace InventoryManagement.Models.RoleModels
{
    public class RoleDetailViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }

        public ActiveEnum IsActive { get; set; }

        public ICollection<UserViewModel> Users { get; set; }
    }
}
