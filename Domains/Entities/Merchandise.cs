using InventoryManagement.Commons.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Domains.Entities
{
    [Table("Merchandises")]
    public class Merchandise
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float Quantity { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        [DefaultValue(ActiveEnum.Active)]
        public ActiveEnum IsActive { get; set; }

        public ICollection<MerchandisePurchaseInvoice> MerchandisePurchaseInvoices { get; set; }
        public ICollection<MerchandiseSaleInvoice> MerchandiseSaleInvoices { get; set; }
    }
}
