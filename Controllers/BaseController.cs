using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
