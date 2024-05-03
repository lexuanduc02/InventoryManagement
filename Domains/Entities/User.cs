using InventoryManagement.Commons.Enums;
using InventoryManagement.Domains.Contractors;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Domains.Entities
{
    [Table("Users")]
    public class User : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public ChangePasswordEnum ChangePassword { get; set; } = ChangePasswordEnum.Unchanged;
        public RecoveryPasswordEnum RecoveryPassword { get; set; } = RecoveryPasswordEnum.UnRequest;
        public DateTime? Dob { get; set; }
        public GenderEnum Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public DateTime? StartDateOfEmployment { get; set; }

        [DefaultValue(ActiveEnum.Active)]
        public ActiveEnum IsActive { get; set; }

        public ICollection<SaleInvoice> SaleInvoices { get; set; }
        public ICollection<PurchaseInvoice> PurchaseInvoices { get; set; }
    }
}
