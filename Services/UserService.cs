using AutoMapper;
using InventoryManagement.Commons.Enums;
using InventoryManagement.Domains.EF;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.UserModels;
using InventoryManagement.Services.Contractors;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net;

namespace InventoryManagement.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponseModel<List<UserViewModel>>> All()
        {
            var response = new ServiceResponseModel<List<UserViewModel>>()
            {
                isSuccess = false,
            };

            try
            {
                var query = from u in _context.Users
                            join r in _context.Roles
                            on u.RoleId equals r.Id
                            select new { u, RoleName = r.Name, RoleId = r.Id };

                var data = await query
                    .Select(x => new UserViewModel
                    {
                        Id = x.u.Id.ToString(),
                        FullName = x.u.FullName,
                        RoleId = x.RoleId.ToString(),
                        RoleName = x.RoleName,
                        Dob = x.u.Dob,
                        Sex = x.u.Sex,
                        PhoneNumber = x.u.PhoneNumber,
                        Address = x.u.Address,
                        Email = x.u.Email,
                        StartDateOfEmployment = x.u.StartDateOfEmployment,
                        IsActive = x.u.IsActive,
                    })
                    .ToListAsync();

                if (data == null)
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

        public async Task<ServiceResponseModel<bool>> Create(CreateUserRequest request)
        {
            var response = new ServiceResponseModel<bool>()
            {
                isSuccess = false,
            };

            try
            {
                var user = _mapper.Map<User>(request);

                _context.Users.Add(user);

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

        public Task<ServiceResponseModel<bool>> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponseModel<UserViewModel>> Get(string id)
        {
            var response = new ServiceResponseModel<UserViewModel>()
            {
                isSuccess = false,
            };

            try
            {
                var query = from u in _context.Users
                            where u.Id.ToString() == id && u.IsActive == ActiveEnum.Active
                            join r in _context.Roles
                            on u.RoleId equals r.Id
                            select new { u , RoleName = r.Name};

                if (!query.Any())
                    return response;

                var data = await query.FirstOrDefaultAsync();

                response.isSuccess = true;
                response.data = new UserViewModel()
                {
                    Id = data.u.Id.ToString(),
                    FullName = data.u.FullName,
                    RoleId = data.u.RoleId.ToString(),
                    RoleName = data.RoleName,
                    Dob = data.u.Dob,
                    Sex = data.u.Sex,
                    PhoneNumber = data.u.PhoneNumber,
                    Address = data.u.Address,
                    Email = data.u.Email,
                    StartDateOfEmployment = data.u.StartDateOfEmployment,
                    IsActive = data.u.IsActive,
                };

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<bool>> Update(UpdateUserRequest request)
        {
            var response = new ServiceResponseModel<bool>()
            {
                isSuccess = false,
            };

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == request.Id && x.IsActive == ActiveEnum.Active);

                if(user == null) 
                {
                    response.Message = "Không tìm thấy thông tin nhân viên!";
                    return response;
                }

                _mapper.Map(request, user);

                _context.Users.Update(user);

                var result = await _context.SaveChangesAsync();

                if(result == 1)
                    response.isSuccess = true;

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }
    }
}
