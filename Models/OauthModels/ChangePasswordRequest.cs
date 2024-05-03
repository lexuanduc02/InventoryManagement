namespace InventoryManagement.Models.OauthModels
{
    public class ChangePasswordRequest
    {
        public string UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set;}
        public string? ReturnUrl { get; set; }
    }
}
