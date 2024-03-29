using InventoryManagement.Commons.Enums;

namespace InventoryManagement.Models.UserModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string RoleId { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public DateTime? Dob { get; set; }
        public GenderEnum Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public DateTime? StartDateOfEmployment { get; set; }
        public ActiveEnum IsActive { get; set; }
    }
}
