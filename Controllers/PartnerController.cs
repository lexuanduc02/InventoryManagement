using InventoryManagement.Commons.Extensions;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.PartnerModels;
using InventoryManagement.Services.Contractors;
using InventoryManagement.Ultility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Authorize(Policy = "admin")]
    public class PartnerController : Controller
    {
        private readonly IPartnerService _partnerService;
        private readonly IPurchaseInvoiceService _purchaseInvoiceService;
        private readonly ISaleInvoiceService _saleInvoiceService;

        public PartnerController(IPartnerService partnerService, 
            IPurchaseInvoiceService purchaseInvoiceService, 
            ISaleInvoiceService saleInvoiceService)
        {
            _partnerService = partnerService;
            _purchaseInvoiceService = purchaseInvoiceService;
            _saleInvoiceService = saleInvoiceService;
        }

        [Breadcrumb("", "Đối tác")]
        public async Task<IActionResult> Index()
        {
            if (TempData["ToastNotify"] != null)
            {
                ToastViewModel tempData = TempData.Get<ToastViewModel>("ToastNotify");
                ViewData["ToastNotify"] = tempData;
            }

            var response = await _partnerService.All();

            return View(response.data);
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var response = await _partnerService.All();

            if (response.data != null)
            {
                return Ok(response.data);
            }

            return BadRequest();
        }

        [Breadcrumb("Thông tin chi tiết", "Đối tác")]
        public async Task<IActionResult> Detail(string id)
        {
            var partner = await _partnerService.Get(id);
            var purchaseInvoice = await _purchaseInvoiceService.GetByPartnerIdAsync(id);
            var returnInvoice = await _saleInvoiceService.GetReturnInvoiceByPartnerIdAsync(id);

            if (!partner.isSuccess || !purchaseInvoice.isSuccess || !returnInvoice.isSuccess)
            {
                TempData.Put("ToastNotify", new ToastViewModel()
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy thông tin!",
                });

                return RedirectToAction(nameof(Index));
            }

            var model = new PartnerDetailViewModel()
            {
                Partner = partner.data,
                PurchaseInvoices = purchaseInvoice.data,
                ReturnInvoices = returnInvoice.data,
            };

            return View(model);
        }

        [Breadcrumb("Thêm mới", "Đối tác")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePartnerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var response = await _partnerService.Create(request);

            TempData.Put("ToastNotify", new ToastViewModel()
            {
                IsSuccess = response.isSuccess,
                Message = response.Message,
            });

            if (!response.isSuccess)
            {
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        [Breadcrumb("Cập nhật", "Đối tác")]
        public async Task<IActionResult> Update(string id)
        {
            var res = await _partnerService.Get(id);

            if(!res.isSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            var partner = res.data;

            var data = new UpdatePartnerRequest()
            {
                Id = id,
                FullName = partner.FullName,
                Company = partner.Company,
                PhoneNumber = partner.PhoneNumber,
                Email = partner.Email,
                Address = partner.Address,
                IsActive = partner.IsActive,
            };

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePartnerRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var response = await _partnerService.Update(request);

            TempData.Put("ToastNotify", new ToastViewModel()
            {
                IsSuccess = response.isSuccess,
                Message = response.Message,
            });

            if (!response.isSuccess)
            {
                ModelState.AddModelError("", response.Message != null ? response.Message : "");
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var res = await _partnerService.Delete(id);

            TempData.Put("ToastNotify", new ToastViewModel()
            {
                IsSuccess = res.isSuccess,
                Message = res.Message,
            });

            return RedirectToAction(nameof(Index));
        }
    }
}
