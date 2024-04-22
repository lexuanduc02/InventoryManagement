using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.ReportModels;

namespace InventoryManagement.Services.Contractors
{
    public interface IReportService 
    {
        Task<ServiceResponseModel<List<MonthlyProductReportViewModel>>> MonthlyProductReport(DateTime date);
        Task<ServiceResponseModel<PurchaseReportViewModel>> PurchaseReport(DateTime? startDate, DateTime? endDate);
    }
}
