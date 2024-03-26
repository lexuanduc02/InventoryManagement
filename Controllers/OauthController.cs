using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class OauthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
