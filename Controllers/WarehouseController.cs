using InventoryManagement.Models.WarehouseModels;
using InventoryManagement.Services.Contractors;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _warehouseService.All();

            return View(response.data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWarehouseRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var response = await _warehouseService.Create(request);

            if (!response.isSuccess)
            {
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var data = await _warehouseService.Get(id);

            return View(data.data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateWarehouseRequest request)
        {
            if(!ModelState.IsValid)
                return View(request);

            var response = await _warehouseService.Update(request);

            if(!response.isSuccess)
            {
                ModelState.AddModelError("", response.Message != null ? response.Message : "");
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id) 
        {   
            var res = await _warehouseService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
