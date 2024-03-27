using AutoMapper;
using Azure.Core;
using InventoryManagement.Domains.EF;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.WarehouseModels;
using InventoryManagement.Services.Contractors;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public WarehouseService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponseModel<List<WarehouseViewModel>>> All()
        {
            var response = new ServiceResponseModel<List<WarehouseViewModel>> ()
            {
                isSuccess = false,
            };

            try
            {
                var warehouses = await _context.Warehouses.Select(x => new WarehouseViewModel()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    StorageCapacity = x.StorageCapacity,
                    Area = x.Area,
                    Unit = x.Unit,
                    Description = x.Description,
                }).ToListAsync();

                response.isSuccess = true;
                response.data = warehouses;

                return response;
            }
            catch (Exception)
            {
                response.isSuccess = false;

                return response;
            }
        }

        public async Task<ServiceResponseModel<bool>> Create(CreateWarehouseRequest request)
        {
            var response = new ServiceResponseModel<bool>()
            {
                isSuccess = false,
            };

            try
            {
                var warehouse = new Warehouse();

                _mapper.Map(request, warehouse);

                _context.Warehouses.Add(warehouse);

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
                var warehouse = await _context.Warehouses.FirstOrDefaultAsync(x => x.Id.ToString() == id);

                if (warehouse == null)
                    return response;

                _context.Warehouses.Remove(warehouse);
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

        public async Task<ServiceResponseModel<WarehouseViewModel>> Get(string id)
        {
            var response = new ServiceResponseModel<WarehouseViewModel>()
            {
                isSuccess = false,
            };

            try
            {
                var warehouse = await _context.Warehouses.FirstOrDefaultAsync(x => x.Id.ToString() == id);

                if (warehouse == null)
                    return response;

                var data = _mapper.Map(warehouse, new WarehouseViewModel());

                response.isSuccess = true;
                response.data = data;

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<bool>> Update(UpdateWarehouseRequest request)
        {
            var response = new ServiceResponseModel<bool>()
            {
                isSuccess = false,
            };

            try
            {
                var warehouse = await _context.Warehouses.FirstOrDefaultAsync(x => x.Id.ToString() == request.Id);

                if(warehouse == null)
                {
                    response.isSuccess = false;
                    response.Message = "Không tìm thấy thông tin kho";

                    return response;
                }

                _mapper.Map(request, warehouse);

                _context.Warehouses.Update(warehouse);
                var result = await _context.SaveChangesAsync();

                if(result == 1)
                {
                    response.isSuccess = true;
                    return response;
                }

                return response;
            }
            catch (Exception)
            {
                response.isSuccess = false;
                response.Message = "Có lỗi trong chương trình!";

                return response;
            }
        }
    }
}
