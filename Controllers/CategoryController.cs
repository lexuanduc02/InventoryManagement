using InventoryManagement.Models.CategoryModels;
using InventoryManagement.Services.Contractors;
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

        public async Task<IActionResult> Index()
        {
            var response = await _categoryService.All();

            return View(response.data);
        }

        public IActionResult Create() 
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequest request)
        {
            if(!ModelState.IsValid) 
            {
                return View(request);
            }

            var response = await _categoryService.Create(request);

            if (!response.isSuccess)
            {
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var res = await _categoryService.Get(id);

            if(!res.isSuccess)
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

            var response = await _categoryService.Update(request);

            if (!response.isSuccess)
            {
                ModelState.AddModelError("", response.Message != null ? response.Message : "");
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var res = await _categoryService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
