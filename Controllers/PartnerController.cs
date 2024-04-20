using InventoryManagement.Models.PartnerModels;
using InventoryManagement.Services.Contractors;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
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

        public async Task<IActionResult> Index()
        {
            var response = await _partnerService.All();

            return View(response.data);
        }

        public async Task<IActionResult> All()
        {
            var response = await _partnerService.All();

            if (response.data != null)
            {
                return Ok(response.data);
            }

            return BadRequest();
        }

        public async Task<IActionResult> Detail(string id)
        {
            var partner = await _partnerService.Get(id);
            var purchaseInvoice = await _purchaseInvoiceService.GetByPartnerIdAsync(id);
            var returnInvoice = await _purchaseInvoiceService.GetByPartnerIdAsync(id, Commons.Enums.InvoiceTypeEnum.ReturnInvoice);

            if (!partner.isSuccess || !purchaseInvoice.isSuccess || !returnInvoice.isSuccess)
                return RedirectToAction(nameof(Index));

            var model = new PartnerDetailViewModel()
            {
                Partner = partner.data,
                PurchaseInvoices = purchaseInvoice.data,
                ReturnInvoices = returnInvoice.data,
            };

            return View(model);
        }

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

            if (!response.isSuccess)
            {
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

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

            return RedirectToAction(nameof(Index));
        }
    }
}
