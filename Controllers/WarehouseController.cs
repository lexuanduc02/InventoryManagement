using InventoryManagement.Models.ProductModels;
using InventoryManagement.Models.WarehouseModels;
using InventoryManagement.Services.Contractors;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IReportService _reportService;
        private readonly IProductService _productService;
        private readonly IPurchaseInvoiceService _purchaseInvoiceService;   
        private readonly IMerchandisePurchaseInvoiceService _merchandisePurchaseInvoiceService;

        public WarehouseController(IWarehouseService warehouseService,
            IReportService reportService,
            IProductService productService,
            IPurchaseInvoiceService purchaseInvoiceService,
            IMerchandisePurchaseInvoiceService merchandisePurchaseInvoiceService)
        {
            _warehouseService = warehouseService;
            _reportService = reportService;
            _productService = productService;
            _purchaseInvoiceService = purchaseInvoiceService;
            _merchandisePurchaseInvoiceService = merchandisePurchaseInvoiceService;
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

        [HttpGet("product")]
        public async Task<IActionResult> ProductReport()
        {
            var res = await _productService.All();

            return View(res.data);
        }

        [HttpGet("product-monthly")]
        public async Task<IActionResult> MonthlyProductReport(DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                date = DateTime.Now;
            }

            var res = await _reportService.MonthlyProductReport(date);

            ViewBag.Date = date;
            return View(res.data);
        }

        public async Task<IActionResult> UpdateWarehouse()
        {
            var res = await _purchaseInvoiceService.AllUnUpdateWarehouseInvoiceAsync();

            var data = res.data;

            return View(data);
        }

        public async Task<IActionResult> ConfirmUpdateWarehouse(string id)
        {
            var getInvoiceRes = await _purchaseInvoiceService.GetAsync(id);
            if (!getInvoiceRes.isSuccess || getInvoiceRes.data == null)
                return RedirectToAction(nameof(Index));
            var invoice = getInvoiceRes.data;

            var getInvoiceDetailRes = await _merchandisePurchaseInvoiceService.GetByInvoiceAsync(id);
            if (!getInvoiceDetailRes.isSuccess || getInvoiceDetailRes.data == null)
                return RedirectToAction(nameof(Index));

            var invoiceDetails = getInvoiceDetailRes.data;

            var data = new UpdateInventoryRequest()
            {
                PurchaseInvoiceViewModel = invoice,
                MerchandisePurchaseViewModels = invoiceDetails,
            };

            return View(data);
        }

        [HttpPost()]
        public async Task<IActionResult> UpdateWarehouse(UpdateInventoryRequest request)
        {

            var res = await _warehouseService.UpdateInventoryAsync(request);

            return RedirectToAction("index", "product");
        }
    }
}
