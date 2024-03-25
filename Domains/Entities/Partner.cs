using InventoryManagement.Commons.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Domains.Entities
{
    [Table("Partners")]
    public class Partner
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public AccountEnum Status { get; set; }

        public ICollection<PurchaseInvoice> PurchaseInvoices { get; set; }
    }
}
