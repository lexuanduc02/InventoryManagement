using InventoryManagement.Commons.Enums;
using InventoryManagement.Models.PurchaseInvoiceModels;
using InventoryManagement.Services.Contractors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Authorize(Policy = "purchase")]
    public class PurchaseInvoiceController : Controller
    {
        private readonly IPurchaseInvoiceService _purchaseInvoiceService;
        private readonly IMerchandisePurchaseInvoiceService _merchandisePurchaseInvoiceService;
        private readonly IPartnerService _partnerService;

        public PurchaseInvoiceController(IPurchaseInvoiceService purchaseInvoiceService, 
            IPartnerService partnerService,
            IMerchandisePurchaseInvoiceService merchandisePurchaseInvoiceService)
        {
            _purchaseInvoiceService = purchaseInvoiceService;
            _partnerService = partnerService;
            _merchandisePurchaseInvoiceService = merchandisePurchaseInvoiceService;
        }

        public async Task<IActionResult> Detail(string id)
        {
            var getInvoiceRes = await _purchaseInvoiceService.GetAsync(id);
            if (!getInvoiceRes.isSuccess || getInvoiceRes.data == null)
                return RedirectToAction(nameof(Index));
            var invoice = getInvoiceRes.data;

            var getInvoiceDetailRes = await _merchandisePurchaseInvoiceService.GetByInvoiceAsync(id);
            if (!getInvoiceDetailRes.isSuccess || getInvoiceDetailRes.data == null)
                return RedirectToAction(nameof(Index));

            var invoiceDetails = getInvoiceDetailRes.data;

            var data = new DetailPurchaseInvoiceModel()
            {
                PurchaseInvoiceViewModel = invoice,
                MerchandisePurchaseViewModels = invoiceDetails,
            };

            return View(data);
        }

        public async Task<IActionResult> Index()
        {
            var res = await _purchaseInvoiceService.AllAsync(InvoiceTypeEnum.Invoice);

            var data = res.data;

            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            var partners = await _partnerService.All();
            ViewBag.Partners = partners.data;

            return View();
        }

        public async Task<IActionResult> Confirm(CreatePurchaseInvoiceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", request);
            }

            var partners = await _partnerService.All();
            ViewBag.Partners = partners.data;

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePurchaseInvoiceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var res = await _purchaseInvoiceService.CreateAsync(request);

            if (!res.isSuccess)
            {
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var getInvoiceRes = await _purchaseInvoiceService.GetAsync(id);
            if (!getInvoiceRes.isSuccess || getInvoiceRes.data == null)
                return RedirectToAction(nameof(Index));
            var invoice = getInvoiceRes.data;

            var getInvoiceDetailRes = await _merchandisePurchaseInvoiceService.GetByInvoiceAsync(id);
            if (!getInvoiceDetailRes.isSuccess || getInvoiceDetailRes.data == null)
                return RedirectToAction(nameof(Index));

            var invoiceDetails = getInvoiceDetailRes.data;

            var data = new UpdatePurchaseInvoiceRequest()
            {
                Id = id,
                UserName = invoice.UserName,
                PartnerId = invoice.PartnerId,
                PaymentMethod = invoice.PaymentMethod,
                Status = invoice.Status,
                Note = invoice.Note,
                UpdateAt = invoice.UpdateAt,
                MerchandisePurchaseViewModels = invoiceDetails,
            };

            var partners = await _partnerService.All();
            ViewBag.Partners = partners.data;

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdatePurchaseInvoiceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Update), new { id = request.Id });
            }

            var res = await _purchaseInvoiceService.UpdateAsync(request);

            if (!res.isSuccess)
            {
                return RedirectToAction(nameof(Update), new { id = request.Id });
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var res = await _purchaseInvoiceService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Return()
        {
            var res = await _purchaseInvoiceService.AllAsync(InvoiceTypeEnum.ReturnInvoice);

            var data = res.data;

            return View(data);
        }

        public async Task<IActionResult> CreateReturn()
        {
            var partners = await _partnerService.All();
            ViewBag.Partners = partners.data;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReturn(CreatePurchaseInvoiceReturnRequest request)
        {
            if (!ModelState.IsValid)
            {
                var partners = await _partnerService.All();
                ViewBag.Partners = partners.data;

                return View(request);
            }

            var res = await _purchaseInvoiceService.CreateReturnAsync(request);

            if (!res.isSuccess)
            {
                return View(request);
            }

            return RedirectToAction(nameof(Return));
        }
    }
}
