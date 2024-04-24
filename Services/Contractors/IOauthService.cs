using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.OauthModels;
using InventoryManagement.Models.UserModels;

namespace InventoryManagement.Services.Contractors
{
    public interface IOauthService
    {
        public Task<ServiceResponseModel<UserViewModel>> LoginAsync(LoginRequest request);
        public Task<ServiceResponseModel<bool>> ChangePasswordAsync(ChangePasswordRequest request);
        public Task<ServiceResponseModel<bool>> ForgotPasswordAsync(ForgotPasswordRequest request);
    }
}
