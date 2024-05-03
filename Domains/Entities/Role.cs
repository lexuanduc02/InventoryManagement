using InventoryManagement.Commons.Enums;
using InventoryManagement.Domains.Contractors;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Domains.Entities
{
    [Table("Roles")]
    public class Role : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }

        public ActiveEnum IsActive { get; set; }

        public bool IsAdmin { get; set; } = false;

        public ICollection<User> Users { get; set; }
    }
}
