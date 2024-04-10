using InventoryManagement.Models.MerchandiseModels;
using InventoryManagement.Services.Contractors;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWarehouseService _warehouseService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, 
            IWarehouseService warehouseService, 
            ICategoryService categoryService)
        {
            _productService = productService;
            _warehouseService = warehouseService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> All()
        {
            var res = await _productService.All();
            return Ok(res.data);
        }

        public async Task<IActionResult> Index()
        {
            var res = await _productService.All();
            return View(res.data);
        }
         
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

            if (!res.isSuccess)
            {
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

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

            if (!res.isSuccess)
                return View(request);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var res = await _productService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
