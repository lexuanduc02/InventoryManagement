using InventoryManagement.Models.CustomerModels;
using InventoryManagement.Services.Contractors;
using InventoryManagement.Ultility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Authorize(Policy = "admin")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IPurchaseInvoiceService _purchaseInvoiceService;
        private readonly ISaleInvoiceService _saleInvoiceService;

        public CustomerController(ICustomerService customerService,
            ISaleInvoiceService saleInvoiceService,
            IPurchaseInvoiceService purchaseInvoiceService)
        {
            _customerService = customerService;
            _saleInvoiceService = saleInvoiceService;
            _purchaseInvoiceService = purchaseInvoiceService;
        }

        [Breadcrumb("", "Khách hàng")]
        public async Task<IActionResult> Index()
        {
            var res = await _customerService.All();

            return View(res.data);
        }

        [Breadcrumb("Thông tin chi tiết", "Khách hàng")]
        public async Task<IActionResult> Detail(string id)
        {
            var customer = await _customerService.GetAsync(id);
            var invoices = await _saleInvoiceService.GetByCustomerIdAsync(id);
            var returnInvoices = await _purchaseInvoiceService.GetReturnInvoiceByCustomerIdAsync(id);

            if (!customer.isSuccess || !invoices.isSuccess || !returnInvoices.isSuccess)
                return RedirectToAction(nameof(Index));

            var model = new CustomerDetailViewModel()
            {
                Customer = customer.data,
                Invoice = invoices.data,
                ReturnInvoice = returnInvoices.data,
            };

            return View(model);
        }

        [Breadcrumb("Thêm mới", "Khách hàng")]
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

        [Breadcrumb("Cập nhật", "Khách hàng")]
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
