using InventoryManagement.Models.SaleInvoiceModels;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class SaleInvoiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSaleInvoiceRequest request)
        {
            return View();
        }
    }
}
