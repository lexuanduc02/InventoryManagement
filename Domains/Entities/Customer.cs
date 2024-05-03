using InventoryManagement.Commons.Enums;
using InventoryManagement.Domains.Contractors;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Domains.Entities
{
    [Table("Customers")]
    public class Customer : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }

        [DefaultValue(ActiveEnum.Active)]
        public ActiveEnum IsActive { get; set; }

        public ICollection<SaleInvoice> SaleInvoices { get; set; }
    }
}
