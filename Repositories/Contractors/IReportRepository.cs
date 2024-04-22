using InventoryManagement.Domains.EF;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Models.ReportModels;

namespace InventoryManagement.Repositories.Contractors
{
    public interface IReportRepository : IRepository<Merchandise, Guid, DataContext>
    {
        Task<List<MonthlyProductReportViewModel>> MonthlyProductReport(DateTime date);
        Task<List<ProductExportImportDetail>> PurchaseReport(DateTime? startDate, DateTime? endDate);
    }
}
