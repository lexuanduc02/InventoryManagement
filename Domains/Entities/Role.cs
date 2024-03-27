using InventoryManagement.Commons.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Domains.Entities
{
    [Table("Roles")]
    public class Role
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }

        [DefaultValue(ActiveEnum.Active)]
        public ActiveEnum IsActive { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
