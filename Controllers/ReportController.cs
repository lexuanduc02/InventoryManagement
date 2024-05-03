using InventoryManagement.Services.Contractors;
using InventoryManagement.Ultility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventoryManagement.Controllers
{
    [Authorize(Policy = "report")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IPartialViewService _partialViewService;
        private readonly IPdfService _pdfService;

        public ReportController(IReportService reportService,
            IPartialViewService partialViewService,
            IPdfService pdfService)
        {
            _reportService = reportService;
            _partialViewService = partialViewService;
            _pdfService = pdfService;
        }

        [Breadcrumb("Báo cáo nhập kho", "Báo cáo")]
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

        public async Task<IActionResult> PurchaseReportToPdf(DateTime startDate, DateTime endDate)
        {
            if (startDate == DateTime.MinValue)
                startDate = DateTime.Now;

            if (endDate == DateTime.MinValue)
                endDate = startDate.AddMonths(-1);

            var res = await _reportService.PurchaseReport(startDate, endDate);

            var html = await _partialViewService.RenderPartialToStringAsync("InventoryInboundReport", res.data);
            var bytes = await _pdfService.HtmlToPdf(html);

            return File(bytes, "application/pdf", "inventory_inbound_Report.pdf");
        }

        [Breadcrumb("Báo cáo xuất kho", "Báo cáo")]
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

        public async Task<IActionResult> SaleReportToPdf(DateTime startDate, DateTime endDate)
        {
            if (startDate == DateTime.MinValue)
                startDate = DateTime.Now;

            if (endDate == DateTime.MinValue)
                endDate = startDate.AddMonths(-1);

            var res = await _reportService.SaleReport(startDate, endDate);

            var html = await _partialViewService.RenderPartialToStringAsync("InventoryOutboundReport", res.data);
            var bytes = await _pdfService.HtmlToPdf(html);

            return File(bytes, "application/pdf", "inventory_outbound_Report.pdf");
        }

        [Breadcrumb("Báo cáo doanh thu", "Báo cáo")]
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


        public async Task<IActionResult> ExportSaleReport2ToPdf(DateTime startDate, DateTime endDate)
        {
            if (startDate == DateTime.MinValue)
                startDate = DateTime.Now;

            if (endDate == DateTime.MinValue)
                endDate = startDate.AddMonths(-1);

            var userName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            var res = await _reportService.SaleReport(startDate, endDate);

            res.data.ReportAuthor = userName;

            var html = await _partialViewService.RenderPartialToStringAsync("SaleReportToPdf", res.data);
            var bytes = await _pdfService.HtmlToPdf(html);

            return File(bytes, "application/pdf", "revenue_report.pdf");
        }

        [Breadcrumb("Báo cáo tồn kho", "Báo cáo")]
        public async Task<IActionResult> InventoryReport()
        {
            var res = await _reportService.InventoryReport();

            if(!res.isSuccess)
                return RedirectToAction("Index", "Home");

            return View(res.data);
        }

        public async Task<IActionResult> InventoryReportToPdf()
        {
            var res = await _reportService.InventoryReport();

            var html = await _partialViewService.RenderPartialToStringAsync("InventoryReportToPdf", res.data);
            var bytes = await _pdfService.HtmlToPdf(html);

            return File(bytes, "application/pdf", "inventory_report.pdf");
        }
    }
}
