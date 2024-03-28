using InventoryManagement.Commons.Enums;

namespace InventoryManagement.Models.PartnerModels
{
    public class UpdatePartnerRequest
    {
        public string Id { get; set; }

        public string FullName { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }

        public ActiveEnum IsActive { get; set; }
    }
}
