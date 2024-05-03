using InventoryManagement.Commons.Extensions;
using InventoryManagement.Domains.EF;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.OauthModels;
using InventoryManagement.Models.UserModels;
using InventoryManagement.Services.Contractors;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class OauthService : IOauthService
    {
        private readonly DataContext _context;

        public OauthService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponseModel<UserViewModel>> LoginAsync(LoginRequest request)
        {
            var response = new ServiceResponseModel<UserViewModel>()
            {
                Message = "Đăng nhập thất bại",
                isSuccess = false,
            };

            try
            {
                var user = await _context.Users
                    .Include(x => x.Role)
                    .Where(x => x.IsActive == Commons.Enums.ActiveEnum.Active 
                        && x.Username == request.Username)
                    .FirstOrDefaultAsync();

                if(user == null)
                {
                    response.Message = "Tài khoản không tồn tại!";
                    return response;
                }

                var enteredPassword = request.Password.HashPassword(user.Salt);

                if(enteredPassword != user.Password)
                {
                    response.Message = "Mật khẩu không đúng!";
                    return response;
                }

                response.isSuccess = true;
                response.Message = "Đăng nhập thành công"!;
                response.data = new UserViewModel()
                {
                    Id = user.Id.ToString(),
                    RoleName = user.Role.Name,
                    FullName = user.FullName,
                    ChangePassword = user.ChangePassword,
                };

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<bool>> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var response = new ServiceResponseModel<bool>()
            {
                Message = "Đổi mật khẩu không thành công!",
                isSuccess = false,
            };

            try
            {
                if(request.NewPassword != request.ConfirmPassword)
                {
                    response.Message = "Mật khẩu mới không trùng khớp";
                    return response;
                }

                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == request.UserId);
                if (user == null)
                {
                    response.Message = "Tài khoản không tồn tại!";
                    return response;
                }

                var enteredPassword = request.OldPassword.HashPassword(user.Salt);

                if (enteredPassword != user.Password)
                {
                    response.Message = "Mật khẩu không đúng!";
                    return response;
                }

                var newPassword = request.NewPassword.HashPassword(user.Salt);

                user.Password = newPassword;
                user.ChangePassword = Commons.Enums.ChangePasswordEnum.Changed;

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

        public async Task<ServiceResponseModel<bool>> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var response = new ServiceResponseModel<bool>()
            {
                Message = "Gửi yêu cầu không thành công!",
                isSuccess = false,
            };

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);

                if (user == null)
                {
                    response.Message = "Số điện thoại chưa được đăng ký!";
                    return response;    
                }

                user.RecoveryPassword = Commons.Enums.RecoveryPasswordEnum.Request;

                _context.Users.Update(user);
                var result = await _context.SaveChangesAsync();

                if(result != 0)
                {
                    response.isSuccess = true;
                    response.Message = "Gửi yêu cầu thành công";
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
