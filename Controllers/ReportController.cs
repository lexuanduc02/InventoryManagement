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

        public async Task<IActionResult> PurchaseReport(DateTime startDate, DateTime endDate)
        {
            if(startDate == DateTime.MinValue)
                startDate = DateTime.Now;

            if (endDate == DateTime.MinValue)
                endDate = startDate.AddMonths(-1);

            var res = await _reportService.PurchaseReport(startDate, endDate);

            if (!res.isSuccess)
                return RedirectToAction("Index", "Home");

            return View(res.data);
        }

        public async Task<IActionResult> SaleReport(DateTime startDate, DateTime endDate)
        {
            if (startDate == DateTime.MinValue)
                startDate = DateTime.Now;

            if (endDate == DateTime.MinValue)
                endDate = startDate.AddMonths(-1);

            var res = await _reportService.SaleReport(startDate, endDate);

            if (!res.isSuccess)
                return RedirectToAction("Index", "Home");

            return View(res.data);
        }

        public async Task<IActionResult> SaleReport2 (DateTime startDate, DateTime endDate)
        {
            if (startDate == DateTime.MinValue)
                startDate = DateTime.Now;

            if (endDate == DateTime.MinValue)
                endDate = startDate.AddMonths(-1);

            var res = await _reportService.SaleReport(startDate, endDate);

            if (!res.isSuccess)
                return RedirectToAction("Index", "Home");

            return View(res.data);
        }

        public async Task<IActionResult> InventoryReport()
        {
            var res = await _reportService.InventoryReport();

            if(!res.isSuccess)
                return RedirectToAction("Index", "Home");

            return View(res.data);
        }
    }
}
