using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.PartnerModels;

namespace InventoryManagement.Services.Contractors
{
    public interface IPartnerService
    {
        public Task<ServiceResponseModel<PartnerViewModel>> Get(string id);
        public Task<ServiceResponseModel<List<PartnerViewModel>>> All();
        public Task<ServiceResponseModel<bool>> Create(CreatePartnerRequest request);
        public Task<ServiceResponseModel<bool>> Update(UpdatePartnerRequest request);
        public Task<ServiceResponseModel<bool>> Delete(string id);
    }
}
