using AutoMapper;
using InventoryManagement.Commons.Enums;
using InventoryManagement.Domains.EF;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.CustomerModels;
using InventoryManagement.Services.Contractors;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class CustomerService : ICustomerService
    {
		private readonly DataContext _context;
		private readonly IMapper _mapper;

        public CustomerService(DataContext context,
			IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
        }

        public async Task<ServiceResponseModel<List<CustomerViewModel>>> All()
        {
			var response = new ServiceResponseModel<List<CustomerViewModel>>()
			{
				isSuccess = false,
			};

			try
			{
				var data = await _context.Customers
					.Where(x => x.IsActive == Commons.Enums.ActiveEnum.Active)
					.Select(x => _mapper.Map<CustomerViewModel>(x))
					.ToListAsync();

                response.isSuccess = true;
                response.data = data;

                return response;
            }
			catch (Exception)
			{
                return response;
            }
        }

        public async Task<ServiceResponseModel<bool>> CreateAsync(CreateCustomerRequest request)
        {
            var response = new ServiceResponseModel<bool>()
            {
                Message = "Cố lỗi trong quá trình tạo khách hàng mới, vui lòng thử lại sau!",
                isSuccess = false,
            };

            try
            {
                var checkExit = await _context.Customers.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);

                if(checkExit != null) 
                {
                    response.Message = "Thông tin khách hàng đã tồn tại!";

                    if(checkExit.IsActive == ActiveEnum.InActive)
                    {
                        checkExit.IsActive = ActiveEnum.Active;

                        _context.Customers.Update(checkExit);

                        await _context.SaveChangesAsync();
                    }

                    return response;
                }

                var newCustomer = _mapper.Map<Customer>(request);

                _context.Customers.Add(newCustomer);
                var result = await _context.SaveChangesAsync();

                if(result == 1)
                {
                    response.Message = "Thêm khách hàng thành công!";
                    response.isSuccess = true;
                }

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<bool>> DeleteAsync(string id)
        {
            var response = new ServiceResponseModel<bool>()
            {
                isSuccess = false,
            };

            try
            {
                var data = await _context.Customers
                    .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.IsActive == ActiveEnum.Active);

                if (data == null)
                {
                    response.isSuccess = false;
                    response.Message = "Không tìm thấy thông tin đối tác!";

                    return response;
                }

                data.IsActive = ActiveEnum.InActive;

                _context.Customers.Update(data);
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

        public async Task<ServiceResponseModel<CustomerViewModel>> GetAsync(string id)
        {
            var response = new ServiceResponseModel<CustomerViewModel>()
            {
                isSuccess = false,
            };

            try
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id.ToString() == id);

                if (customer == null)
                    return response;

                var data = _mapper.Map<CustomerViewModel>(customer);

                response.isSuccess = true;
                response.data = data;

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<bool>> UpdateAsync(UpdateCustomerRequest request)
        {
            var response = new ServiceResponseModel<bool>()
            {
                isSuccess = false,
            };

            try
            {
                var data = await _context.Customers
                    .FirstOrDefaultAsync(x => x.Id.ToString() == request.Id || x.IsActive == ActiveEnum.Active);

                if (data == null)
                {
                    response.isSuccess = false;
                    response.Message = "Không tìm thấy thông tin đối tác!";

                    return response;
                }

                _mapper.Map(request, data);

                _context.Customers.Update(data);
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
