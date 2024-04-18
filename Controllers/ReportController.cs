using InventoryManagement.Services.Contractors;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class ReportController : Controller
    {
        //private readonly IReportService _reportService;
        //private readonly IProductService _productService;

        //public ReportController(IReportService reportService, 
        //    IProductService productService)
        //{
        //    _reportService = reportService;
        //    _productService = productService;
        //}

        //[HttpGet("product")]
        //public async Task<IActionResult> ProductReport()
        //{
        //    var res = await _productService.All();

        //    return View(res.data);
        //}

        //[HttpGet("product-monthly")]
        //public async Task<IActionResult> MonthlyProductReport(DateTime date)
        //{
        //    if(date == DateTime.MinValue)
        //    {
        //        date = DateTime.Now;
        //    }

        //    var res = await _reportService.MonthlyProductReport(date);

        //    ViewBag.Date = date;
        //    return View(res.data);
        //}
    }
}
