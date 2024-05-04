using Azure;
using InventoryManagement.Commons.Extensions;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.MerchandiseModels;
using InventoryManagement.Services.Contractors;
using InventoryManagement.Ultility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWarehouseService _warehouseService;
        private readonly ICategoryService _categoryService;
        private readonly IReportService _reportService;

        public ProductController(IProductService productService, 
            IWarehouseService warehouseService, 
            ICategoryService categoryService, 
            IReportService reportService)
        {
            _productService = productService;
            _warehouseService = warehouseService;
            _categoryService = categoryService;
            _reportService = reportService;
        }

        public async Task<IActionResult> All()
        {
            var res = await _productService.All();
            return Ok(res.data);
        }

        [Authorize(Policy = "productAccess")]
        [Breadcrumb("", "Sản phẩm")]
        public async Task<IActionResult> Index()
        {
            if (TempData["ToastNotify"] != null)
            {
                ToastViewModel tempData = TempData.Get<ToastViewModel>("ToastNotify");
                ViewData["ToastNotify"] = tempData;
            }

            var res = await _productService.All();
            return View(res.data);
        }

        [Authorize(Policy = "warehouse")]
        [Breadcrumb("Thêm mới", "Sản phẩm")]
        public async Task<IActionResult> Create()
        {
            var warehouses = await _warehouseService.All();
            var categories = await _categoryService.All();

            ViewBag.Warehouses = warehouses.data;
            ViewBag.Categories = categories.data;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request) 
        {
            if (!ModelState.IsValid)
            {
                var warehouses = await _warehouseService.All();
                var categories = await _categoryService.All();

                ViewBag.Warehouses = warehouses.data;
                ViewBag.Categories = categories.data;

                return View(request);
            }

            var res = await _productService.Create(request);

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

        [Authorize(Policy = "warehouse")]
        [Breadcrumb("Cập nhật", "Sản phẩm")]
        public async Task<IActionResult> Update(string id)
        {
            var warehouses = await _warehouseService.All();
            var categories = await _categoryService.All();

            ViewBag.Warehouses = warehouses.data;
            ViewBag.Categories = categories.data;

            var product = (await _productService.Get(id)).data;

            var updateProductRequest = new UpdateProductRequest()
            {
                Id = id,
                CategoryId = product.CategoryId,
                WarehouseId = product.WarehouseId,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Unit = product.Unit,
                Description = product.Description,
                Image = product.Image,
                IsActive = product.IsActive,
            };

            return View(updateProductRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductRequest request)
        {
            if(!ModelState.IsValid) 
                return View(request);

            var res = await _productService.Update(request);

            TempData.Put("ToastNotify", new ToastViewModel()
            {
                IsSuccess = res.isSuccess,
                Message = res.Message,
            });

            if (!res.isSuccess)
                return View(request);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var res = await _productService.Delete(id);

            TempData.Put("ToastNotify", new ToastViewModel()
            {
                IsSuccess = res.isSuccess,
                Message = res.Message,
            });

            return RedirectToAction(nameof(Index));
        }
    }
}
