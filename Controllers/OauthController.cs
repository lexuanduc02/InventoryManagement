using InventoryManagement.Models.OauthModels;
using InventoryManagement.Services.Contractors;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventoryManagement.Controllers
{
    [AllowAnonymous]
    public class OauthController : Controller
    {
        private readonly IOauthService _oauthService;

        public OauthController(IOauthService oauthService)
        {
            _oauthService = oauthService;
        }

        public IActionResult Login(string? ReturnUrl)
        {
            var model = new LoginRequest()
            {
                ReturnUrl = ReturnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if(!ModelState.IsValid) 
            {
                ModelState.AddModelError(string.Empty, "Vui lòng điền đầy đủ thông tin!");
                return View(request);
            }

            var res = await _oauthService.LoginAsync(request);

            if(!res.isSuccess)
            {
                ModelState.AddModelError(string.Empty, res.Message);
                return View(request);
            }

            var user = res.data;

            if (user.ChangePassword == Commons.Enums.ChangePasswordEnum.Unchanged)
            {
                var changePasswordModel = new ChangePasswordRequest()
                {
                    ReturnUrl = request.ReturnUrl,
                    UserId = user.Id.ToString(),
                };

                return RedirectToAction(nameof(ChangePassword), changePasswordModel);
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, user.Id),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Role, user.RoleName),
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );

            return Redirect(request.ReturnUrl ?? "/");
        }

        [Authorize]
        public IActionResult ChangePassword(ChangePasswordRequest request, string ReturlUrl)
        {
            if (request.UserId == null)
                request.UserId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;

            var model = new ChangePasswordRequest()
            {
                ReturnUrl = request.ReturnUrl,
                OldPassword = request.OldPassword,
                UserId = request.UserId,
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Vui lòng điền đầy đủ thông tin!");
                return View(request);
            }

            var res = await _oauthService.ChangePasswordAsync(request);

            if(!res.isSuccess)
            {
                ModelState.AddModelError(string.Empty, res.Message);
                return View(request);
            }

            return RedirectToAction(nameof(Login), new { returnUrl = request.ReturnUrl });
        }

        public async Task<IActionResult> Logout(string ReturnUrl)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Vui lòng điền đầy đủ thông tin!");
                return View(request);
            }

            var res = await _oauthService.ForgotPasswordAsync(request);

            if (!res.isSuccess)
            {
                ModelState.AddModelError(string.Empty, res.Message);
                return View(request);
            }

            return RedirectToAction(nameof(Login));
        }
    }
}
