using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create() 
        {
            return View();
        }
    }
}
