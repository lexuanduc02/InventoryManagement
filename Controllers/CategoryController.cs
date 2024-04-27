using InventoryManagement.Commons.Extensions;
using InventoryManagement.Models.CategoryModels;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Services.Contractors;
using InventoryManagement.Ultility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{

    [Authorize(Policy = "admin")]
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Breadcrumb("", "Nhóm hàng")]
        public async Task<IActionResult> Index()
        {
            var response = await _categoryService.All();

            if (TempData["ToastNotify"] != null)
            {
                var tempData = TempData.Get<ToastViewModel>("ToastNotify");
                ViewData["ToastNotify"] = tempData;
            }

            return View(response.data);
        }

        [Breadcrumb("Thêm mới", "Nhóm hàng")]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequest request)
        {
            if (!ModelState.IsValid) 
            {
                return View(request);
            }

            var res = await _categoryService.Create(request);

            TempData.Put("ToastNotify", new ToastViewModel()
            {
                IsSuccess = res.isSuccess,
                Message = res.Message,
            });

            if (!res.isSuccess)
            {
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        [Breadcrumb("Cập nhật", "Nhóm hàng")]
        public async Task<IActionResult> Update(string id)
        {
            var res = await _categoryService.Get(id);

            TempData.Put("ToastNotify", new ToastViewModel()
            {
                IsSuccess = res.isSuccess,
                Message = res.Message,
            });

            if (!res.isSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            var data = res.data;

            var model = new UpdateCategoryRequest()
            {
                Id = id,
                Name = data.Name,
                Description = data.Description,
                Image = data.Image,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var res = await _categoryService.Update(request);

            TempData.Put("ToastNotify", new ToastViewModel()
            {
                IsSuccess = res.isSuccess,
                Message = res.Message,
            });

            if (!res.isSuccess)
            {
                ModelState.AddModelError("", res.Message != null ? res.Message : "");
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var res = await _categoryService.Delete(id);

            TempData.Put("ToastNotify", new ToastViewModel()
            {
                IsSuccess = res.isSuccess,
                Message = res.Message,
            });

            return RedirectToAction(nameof(Index));
        }
    }
}
