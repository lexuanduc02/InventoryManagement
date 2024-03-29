using AutoMapper;
using InventoryManagement.Commons.Enums;
using InventoryManagement.Domains.EF;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.RoleModels;
using InventoryManagement.Services.Contractors;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class RoleService : IRoleService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RoleService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponseModel<List<RoleViewModel>>> All()
        {
            var response = new ServiceResponseModel<List<RoleViewModel>>()
            {
                isSuccess = false,
            };

            try
            {
                var data = await _context.Roles
                    .Where(x => x.IsAdmin == false && x.IsActive == ActiveEnum.Active)
                    .Select(x => _mapper.Map<RoleViewModel>(x))
                    .ToListAsync();

                if(data == null)
                    return response;

                response.isSuccess = true;
                response.data = data;

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }
    }
}
