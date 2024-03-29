using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.UserModels;

namespace InventoryManagement.Services.Contractors
{
    public interface IUserService
    {
        public Task<ServiceResponseModel<UserViewModel>> Get(string id);
        public Task<ServiceResponseModel<List<UserViewModel>>> All();
        public Task<ServiceResponseModel<bool>> Create(CreateUserRequest request);
        public Task<ServiceResponseModel<bool>> Update(UpdateUserRequest request);
        public Task<ServiceResponseModel<bool>> Delete(string id);
    }
}
