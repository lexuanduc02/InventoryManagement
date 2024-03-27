﻿using AutoMapper;
using InventoryManagement.Commons.Enums;
using InventoryManagement.Domains.EF;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Models.CategoryModels;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.WarehouseModels;
using InventoryManagement.Services.Contractors;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CategoryService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponseModel<List<CategoryViewModel>>> All()
        {
            var response = new ServiceResponseModel<List<CategoryViewModel>>()
            {
                isSuccess = false,
            };

            try
            {
                var warehouses = await _context.Categories
                                    .Where(x => x.IsActive == ActiveEnum.Active)
                                    .Select(x => new CategoryViewModel()
                                    {
                                        Id = x.Id.ToString(),
                                        Name = x.Name,
                                        Description = x.Description,
                                    })
                                    .ToListAsync();

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

        public async Task<ServiceResponseModel<bool>> Create(CreateCategoryRequest request)
        {
            var response = new ServiceResponseModel<bool>()
            {
                isSuccess = false,
            };

            try
            {
                var category = new Category();

                _mapper.Map(request, category);

                _context.Categories.Add(category);

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
                var data = await _context.Categories
                    .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.IsActive == ActiveEnum.Active);

                if (data == null)
                {
                    response.isSuccess = false;
                    response.Message = "Không tìm thấy thông tin ngành hàng!";

                    return response;
                }

                data.IsActive = ActiveEnum.InActive;

                _context.Categories.Update(data);
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

        public async Task<ServiceResponseModel<CategoryViewModel>> Get(string id)
        {
            var response = new ServiceResponseModel<CategoryViewModel>()
            {
                isSuccess = false,
            };

            try
            {
                var data = await _context.Categories
                    .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.IsActive == ActiveEnum.Active);

                if (data == null)
                {
                    response.isSuccess = false;
                    response.Message = "Không tìm thấy thông tin ngành hàng!";

                    return response;
                }

                response.isSuccess = true;
                response.data = _mapper.Map(data, new CategoryViewModel());

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<bool>> Update(UpdateCategoryRequest request)
        {
            var response = new ServiceResponseModel<bool>()
            {
                isSuccess = false,
            };

            try
            {
                var data = await _context.Categories
                    .FirstOrDefaultAsync(x => x.Id.ToString() == request.Id || x.IsActive == ActiveEnum.Active);

                if (data == null)
                {
                    response.isSuccess = false;
                    response.Message = "Không tìm thấy thông tin ngành hàng!";

                    return response;
                }

                _mapper.Map(request, data);

                _context.Categories.Update(data);
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
