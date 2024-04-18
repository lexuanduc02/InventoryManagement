using InventoryManagement.Commons.Enums;
using InventoryManagement.Domains.Contractors;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Domains.Entities
{
    [Table("Warehouses")]
    public class Warehouse : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public float StorageCapacity { get; set; }
        public float? Area { get; set; }
        public string Unit { get; set; }
        public string? Description { get; set; }

        [DefaultValue(ActiveEnum.Active)]
        public ActiveEnum IsActive { get; set; }

        public ICollection<Merchandise> Merchandises { get; set; } = new List<Merchandise>();
    }
}
