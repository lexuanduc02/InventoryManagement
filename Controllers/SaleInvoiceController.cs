using InventoryManagement.Models.SaleInvoiceModels;
using InventoryManagement.Services.Contractors;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class SaleInvoiceController : Controller
    {
        private readonly ISaleInvoiceService _saleInvoiceService;
        private readonly IMerchandiseSaleInvoiceService _merchandiseSaleInvoiceService;

        public SaleInvoiceController(ISaleInvoiceService saleInvoiceService, 
            IMerchandiseSaleInvoiceService merchandiseSaleInvoiceService)
        {
            _saleInvoiceService = saleInvoiceService;
            _merchandiseSaleInvoiceService = merchandiseSaleInvoiceService;
        }

        public async Task<IActionResult> Index()
        {
            var res = await _saleInvoiceService.AllAsync();

            var data = res.data;

            return View(data);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var getInvoiceRes = await _saleInvoiceService.GetAsync(id);
            if (!getInvoiceRes.isSuccess || getInvoiceRes.data == null)
                return RedirectToAction(nameof(Index));

            var invoice = getInvoiceRes.data;

            var getInvoiceDetailRes = await _merchandiseSaleInvoiceService.GetByInvoiceAsync(id);
            if (!getInvoiceDetailRes.isSuccess || getInvoiceDetailRes.data == null)
                return RedirectToAction(nameof(Index));

            var invoiceDetails = getInvoiceDetailRes.data;

            var data = new DetailSaleInvoiceViewModel()
            {
                SaleInvoiceViewModel = invoice,
                MerchandiseSaleInvoiceViewModels = invoiceDetails,
            };

            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSaleInvoiceRequest request)
        {
            if(!ModelState.IsValid)
            {
                return View(request);
            }

            var res = await _saleInvoiceService.CreateAsync(request);

            if(!res.isSuccess)
            {
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
