using InventoryManagement.Commons.Enums;

namespace InventoryManagement.Models.UserModels
{
    public class CreateUserRequest
    {
        public Guid RoleId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } = "nv@" + DateTime.Now.Year;
        public DateTime? Dob { get; set; }
        public GenderEnum Sex { get; set; } = GenderEnum.Unknown;
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public DateTime? StartDateOfEmployment { get; set; } = DateTime.UtcNow;
        public ActiveEnum IsActive { get; set; } = ActiveEnum.Active;
    }
}
