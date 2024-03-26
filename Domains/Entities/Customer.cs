using InventoryManagement.Commons.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Domains.Entities
{
    [Table("Customers")]
    public class Customer
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public AccountEnum Status { get; set; }

        public ICollection<SaleInvoice> SaleInvoices { get; set; }
    }
}
