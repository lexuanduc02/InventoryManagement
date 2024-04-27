using AutoMapper;
using InventoryManagement.Commons.Enums;
using InventoryManagement.Commons.Extensions;
using InventoryManagement.Domains.EF;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.UserModels;
using InventoryManagement.Services.Contractors;
using Microsoft.EntityFrameworkCore;
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
                            where r.Name != AuthorizationEnum.Admin.ToString()
                            select new { u, RoleName = r.Name, RoleId = r.Id, RoleDescription = r.Description };

                var data = await query
                    .Select(x => new UserViewModel
                    {
                        Id = x.u.Id.ToString(),
                        FullName = x.u.FullName,
                        RoleId = x.RoleId.ToString(),
                        RoleName = x.RoleName,
                        RoleDescription = x.RoleDescription,
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

        public async Task<ServiceResponseModel<bool>> ChangePasswordToDefaultAsync(string userId)
        {
            var response = new ServiceResponseModel<bool>()
            {
                Message = "Không tìm thấy thông tin người dùng!",
                isSuccess = false,
            };

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == userId);

                if (user == null) return response;

                var salt = SecurityExtension.GenerateSalt();

                var defaultPassword = ("nv@" + DateTime.Now.Year).HashPassword(salt);

                user.Salt = salt;
                user.Password = defaultPassword;
                user.RecoveryPassword = RecoveryPasswordEnum.UnRequest;
                user.ChangePassword = ChangePasswordEnum.Unchanged;

                _context.Users.Update(user);
                var result = await _context.SaveChangesAsync();

                if(result != 0)
                {
                    response.isSuccess = true;
                    response.Message = "Đổi mật khẩu thành công!";
                }

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
                Message = "Lỗi hệ thống",
                isSuccess = false,
            };

            try
            {
                request.Salt = SecurityExtension.GenerateSalt();

                request.Password = request.Password.HashPassword(request.Salt);

                var user = _mapper.Map<User>(request);

                _context.Users.Add(user);

                var result = await _context.SaveChangesAsync();

                if (result != 0)
                {
                    response.isSuccess = true;
                    response.Message = "Thêm nhân viên thành công";
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
                Message = "Có lỗi hệ thống!",
                isSuccess = false,
            };

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == id);    

                if(user == null)
                {
                    response.Message = "Không tìm thấy thông tin nhân viên!";
                    return response;
                }

                user.IsActive = ActiveEnum.InActive;

                _context.Users.Update(user);
                var result = await _context.SaveChangesAsync();

                if(result != 0)
                {
                    response.isSuccess = true;
                    response.Message = "Xóa nhân viên thành công";
                }

                return response;
            }
            catch (Exception)
            {
                return response;
            }
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
                            where u.Id.ToString() == id
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

        public async Task<ServiceResponseModel<List<UserViewModel>>> GetRecoveryPasswordsAsync()
        {
            var response = new ServiceResponseModel<List<UserViewModel>>()
            {
                isSuccess = false,
            };

            try
            {
                var data = await _context.Users
                    .Where(x => x.RecoveryPassword == RecoveryPasswordEnum.Request)
                    .Select(x => new UserViewModel
                    {
                        Id = x.Id.ToString(),
                        FullName = x.FullName,
                        PhoneNumber = x.PhoneNumber,
                        Email = x.Email,
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

        public async Task<ServiceResponseModel<bool>> Update(UpdateUserRequest request)
        {
            var response = new ServiceResponseModel<bool>()
            {
                Message = "Lỗi hệ thống",
                isSuccess = false,
            };

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == request.Id);

                if(user == null) 
                {
                    response.Message = "Không tìm thấy thông tin nhân viên!";
                    return response;
                }

                _mapper.Map(request, user);

                _context.Users.Update(user);

                var result = await _context.SaveChangesAsync();

                if (result != 0)
                {
                    response.Message = "Cập nhật thành công";
                    response.isSuccess = true;
                }

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }
    }
}
