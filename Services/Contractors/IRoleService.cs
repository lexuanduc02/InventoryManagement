using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.RoleModels;

namespace InventoryManagement.Services.Contractors
{
    public interface IRoleService
    {
        public Task<ServiceResponseModel<List<RoleViewModel>>> All();
    }
}
