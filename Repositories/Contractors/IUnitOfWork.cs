using System.Data.Common;

namespace InventoryManagement.Repositories.Contractors
{
    public interface IUnitOfWork
    {
        DbConnection DbConnection();
        IReportRepository ReportRepository { get; }
        IPurchaseInvoiceRepository PurchaseInvoiceRepository { get; }
    }
}
