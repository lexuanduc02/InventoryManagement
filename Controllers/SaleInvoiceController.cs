using InventoryManagement.Commons.Enums;
using InventoryManagement.Commons.Extensions;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.SaleInvoiceModels;
using InventoryManagement.Services;
using InventoryManagement.Services.Contractors;
using InventoryManagement.Ultility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SelectPdf;

namespace InventoryManagement.Controllers
{
    [Authorize(Policy = "sale")]
    public class SaleInvoiceController : Controller
    {
        private readonly ISaleInvoiceService _saleInvoiceService;
        private readonly IMerchandiseSaleInvoiceService _merchandiseSaleInvoiceService;
        private readonly IPartialViewService _partialViewService;
        private readonly IPdfService _pdfService;
        private readonly IPartnerService _partnerService;

        public SaleInvoiceController(ISaleInvoiceService saleInvoiceService, 
            IMerchandiseSaleInvoiceService merchandiseSaleInvoiceService,
            IPartialViewService partialViewService,
            IPdfService pdfService,
            IPartnerService partnerService)
        {
            _saleInvoiceService = saleInvoiceService;
            _merchandiseSaleInvoiceService = merchandiseSaleInvoiceService;
            _partialViewService = partialViewService;
            _pdfService = pdfService;
            _partnerService = partnerService;
        }

        [Breadcrumb("", "Hóa đơn xuất")]
        public async Task<IActionResult> Index()
        {
            if (TempData["ToastNotify"] != null)
            {
                ToastViewModel tempData = TempData.Get<ToastViewModel>("ToastNotify");
                ViewData["ToastNotify"] = tempData;
            }

            var res = await _saleInvoiceService.AllAsync(InvoiceTypeEnum.Invoice);

            return View(res.data);
        }

        [Breadcrumb("Chi tiết", "Hóa đơn xuất")]
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

            ViewBag.InvoiceType = invoice.InvoiceType;

            return View(data);
        }

        [Breadcrumb("Thêm mới", "Hóa đơn xuất")]
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

            TempData.Put("ToastNotify", new ToastViewModel()
            {
                IsSuccess = res.isSuccess,
                Message = res.Message,
            });

            return RedirectToAction(nameof(Index));
        }

        [Breadcrumb("Xác nhận thông tin hóa đơn", "Hóa đơn xuất")]
        public IActionResult Confirm(CreateSaleInvoiceRequest request)
        {
            if(!ModelState.IsValid)
            {
                return View("Create", request);
            }

            return View(request);
        }

        [Breadcrumb("Cập nhật", "Hóa đơn xuất")]
        public async Task<IActionResult> Update(string id)
        {
            var getInvoiceRes = await _saleInvoiceService.GetAsync(id);
            if (!getInvoiceRes.isSuccess || getInvoiceRes.data == null)
                return RedirectToAction(nameof(Index));

            var invoice = getInvoiceRes.data;

            var getInvoiceDetailRes = await _merchandiseSaleInvoiceService.GetByInvoiceAsync(id);
            if (!getInvoiceDetailRes.isSuccess || getInvoiceDetailRes.data == null)
                return RedirectToAction(nameof(Index));

            var invoiceDetails = getInvoiceDetailRes.data;

            var data = new UpdateSaleInvoiceRequest()
            {
                SaleInvoiceViewModel = invoice,
                MerchandiseSaleInvoiceViewModels = invoiceDetails,
            };

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateSaleInvoiceRequest request)
        {
            if(!ModelState.IsValid) 
            { 
                return RedirectToAction(nameof(Update), new { id = request.SaleInvoiceViewModel.Id });
            }

            var res = await _saleInvoiceService.UpdateAsync(request);

            if(!res.isSuccess)
            {
                return RedirectToAction(nameof(Update), new { id = request.SaleInvoiceViewModel.Id });
            }

            TempData.Put("ToastNotify", new ToastViewModel()
            {
                IsSuccess = res.isSuccess,
                Message = res.Message,
            });

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var res = await _saleInvoiceService.DeleteAsync(id);

            TempData.Put("ToastNotify", new ToastViewModel()
            {
                IsSuccess = res.isSuccess,
                Message = res.Message,
            });

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Return()
        {
            var res = await _saleInvoiceService.AllAsync(InvoiceTypeEnum.ReturnInvoice);
            return View(res.data);
        }

        [Breadcrumb("Thêm mới", "Trả hàng NCC")]
        public async Task<IActionResult> CreateReturn()
        {
            var partners = await _partnerService.All();
            ViewBag.Partners = partners.data;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReturn(CreateSaleReturnInvoiceRequest request)
        {
            if (!ModelState.IsValid)
            {
                var partners = await _partnerService.All();
                ViewBag.Partners = partners.data;
                return View(request);
            }

            var res = await _saleInvoiceService.CreateReturnAsync(request);

            if (!res.isSuccess)
            {
                var partners = await _partnerService.All();
                ViewBag.Partners = partners.data;
                return View(request);
            }

            TempData.Put("ToastNotify", new ToastViewModel()
            {
                IsSuccess = res.isSuccess,
                Message = res.Message,
            });

            return RedirectToAction(nameof(Return));
        }

        public async Task<IActionResult> ExportPdf(string id)
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

            var html = await _partialViewService.RenderPartialToStringAsync("SaleInvoice", data);
            var bytes = await _pdfService.HtmlToPdf(html);

            return File(bytes, "application/pdf", "invoice.pdf");
        }
    }
}
