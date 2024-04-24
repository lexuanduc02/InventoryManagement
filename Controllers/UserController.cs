﻿using InventoryManagement.Models.UserModels;
using InventoryManagement.Services.Contractors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Authorize(Policy = "admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var res = await _userService.All();

            return View(res.data);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = (await _roleService.All()).data;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = (await _roleService.All()).data;
                return View(request);
            }

            var response = await _userService.Create(request);

            if (!response.isSuccess)
            {
                ViewBag.Roles = (await _roleService.All()).data;
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id) 
        {
            var res = await _userService.Get(id);

            if(!res.isSuccess)
                return RedirectToAction(nameof(Index));

            var data = res.data;

            var updateViewModel = new UpdateUserRequest()
            {
                Id = data.Id,
                FullName = data.FullName,
                RoleId = data.RoleId,
                Dob = data.Dob,
                Sex = data.Sex,
                PhoneNumber = data.PhoneNumber,
                Address = data.Address,
                Email = data.Email,
                IsActive = data.IsActive,
            };

            ViewBag.Roles = (await _roleService.All()).data;

            return View(updateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserRequest request)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Roles = (await _roleService.All()).data;
                return View(request);
            }

            var response = await _userService.Update(request);

            if (!response.isSuccess)
            {
                ViewBag.Roles = (await _roleService.All()).data;
                ModelState.AddModelError("", response.Message != null ? response.Message : "");
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var res = await _userService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RecoveryPasswordRequest()
        {
            var res = await _userService.GetRecoveryPasswordsAsync();

            return View(res.data);
        }

        public async Task<IActionResult> ChangePasswordToDefault(string id)
        {
            var res = await _userService.ChangePasswordToDefaultAsync(id);

            return RedirectToAction(nameof(RecoveryPasswordRequest));
        }
    }
}
