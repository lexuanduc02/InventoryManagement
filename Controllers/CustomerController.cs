using InventoryManagement.Models.CustomerModels;
using InventoryManagement.Services.Contractors;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            var res = await _customerService.All();

            return View(res.data);
        }

        public IActionResult Create() 
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerRequest request)
        {
            if(!ModelState.IsValid) { return View(request); }

            var res = await _customerService.CreateAsync(request);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(string id) 
        {
            var customer = (await _customerService.GetAsync(id)).data;

            var updateCustomerRequest = new UpdateCustomerRequest()
            {
                Id = id,
                FullName = customer.FullName,
                Address = customer.Address,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                IsActive = customer.IsActive
            };

            return View(updateCustomerRequest); 
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCustomerRequest request)
        {
            if(!ModelState.IsValid) { return View(request); }

            var res = await _customerService.UpdateAsync(request);

            if(res.isSuccess)
            {
                return RedirectToAction("Index");
            }

            return View(request);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var res = await _customerService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
