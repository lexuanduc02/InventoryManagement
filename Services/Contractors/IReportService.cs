using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.ReportModels;

namespace InventoryManagement.Services.Contractors
{
    public interface IReportService 
    {
        Task<ServiceResponseModel<List<MonthlyProductReportViewModel>>> MonthlyProductReport(DateTime date);
        Task<ServiceResponseModel<InvoiceReportViewModel>> PurchaseReport(DateTime startDate, DateTime endDate);
        Task<ServiceResponseModel<InvoiceReportViewModel>> SaleReport(DateTime startDate, DateTime endDate);
        Task<ServiceResponseModel<List<InventoryReportViewModel>>> InventoryReport();
    }
}
