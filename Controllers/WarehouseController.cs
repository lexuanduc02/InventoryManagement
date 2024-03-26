using InventoryManagement.Models.WarehouseModels;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class WarehouseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(CreateWarehouseRequest request) {
        //    if(!ModelState.IsValid) 
        //    {
        //        return View(request);
        //    }


        //}
    }
}
