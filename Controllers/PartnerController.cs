using InventoryManagement.Models.PartnerModels;
using InventoryManagement.Services.Contractors;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class PartnerController : Controller
    {
        private readonly IPartnerService _partnerService;

        public PartnerController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _partnerService.All();

            return View(response.data);
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
