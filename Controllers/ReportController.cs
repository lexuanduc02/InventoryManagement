using InventoryManagement.Services.Contractors;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<IActionResult> PurchaseReport(DateTime? startdate, DateTime? endDate)
        {
            if(startdate == null)
                startdate = DateTime.Now;

            if(endDate == null) 
                endDate = DateTime.MinValue;

            var res = await _reportService.PurchaseReport(startdate, endDate);

            if (!res.isSuccess)
                return RedirectToAction("Index", "Home");

            return View(res.data);
        }
        
    }
}
