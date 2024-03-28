using AutoMapper;
using InventoryManagement.Commons.Enums;
using InventoryManagement.Domains.EF;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Models.CategoryModels;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.PartnerModels;
using InventoryManagement.Services.Contractors;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PartnerService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponseModel<List<PartnerViewModel>>> All()
        {
            var response = new ServiceResponseModel<List<PartnerViewModel>>()
            {
                isSuccess = false,
            };

            try
            {
                var data = await _context.Partners
                                    .Where(x => x.IsActive == ActiveEnum.Active)
                                    .Select(x => _mapper.Map<PartnerViewModel>(x))
                                    .ToListAsync();

                response.isSuccess = true;
                response.data = data;

                return response;
            }
            catch (Exception)
            {
                response.isSuccess = false;

                return response;
            }
        }

        public async Task<ServiceResponseModel<bool>> Create(CreatePartnerRequest request)
        {
            var response = new ServiceResponseModel<bool>()
            {
                isSuccess = false,
            };

            try
            {
                var newPartner = _mapper.Map<Partner>(request);

                _context.Partners.Add(newPartner);

                var result = await _context.SaveChangesAsync();

                if (result != 1)
                    return response;

                response.isSuccess = true;

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<bool>> Delete(string id)
        {
            var response = new ServiceResponseModel<bool>()
            {
                isSuccess = false,
            };

            try
            {
                var data = await _context.Partners
                    .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.IsActive == ActiveEnum.Active);

                if (data == null)
                {
                    response.isSuccess = false;
                    response.Message = "Không tìm thấy thông tin đối tác!";

                    return response;
                }

                data.IsActive = ActiveEnum.InActive;

                _context.Partners.Update(data);
                var result = await _context.SaveChangesAsync();

                if (result != 1)
                    return response;

                response.isSuccess = true;
                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<PartnerViewModel>> Get(string id)
        {
            var response = new ServiceResponseModel<PartnerViewModel>()
            {
                isSuccess = false,
            };

            try
            {
                var data = await _context.Partners
                    .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.IsActive == ActiveEnum.Active);

                if (data == null)
                {
                    response.isSuccess = false;
                    response.Message = "Không tìm thấy thông tin đối tác!";

                    return response;
                }

                response.isSuccess = true;
                response.data = _mapper.Map(data, new PartnerViewModel());

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<bool>> Update(UpdatePartnerRequest request)
        {
            var response = new ServiceResponseModel<bool>()
            {
                isSuccess = false,
            };

            try
            {
                var data = await _context.Partners
                    .FirstOrDefaultAsync(x => x.Id.ToString() == request.Id || x.IsActive == ActiveEnum.Active);

                if (data == null)
                {
                    response.isSuccess = false;
                    response.Message = "Không tìm thấy thông tin đối tác!";

                    return response;
                }

                _mapper.Map(request, data);

                _context.Partners.Update(data);
                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    response.isSuccess = true;
                    return response;
                }

                return response;
            }
            catch (Exception)
            {
                response.isSuccess = false;
                response.Message = "Có lỗi trong khi thực hiện chương trình!";

                return response;
            }
        }
    }
}
