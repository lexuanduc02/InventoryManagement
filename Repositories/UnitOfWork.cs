using InventoryManagement.Domains.EF;
using InventoryManagement.Repositories.Contractors;
using System.Data.Common;

namespace InventoryManagement.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;

            ReportRepository = new ReportRepository(context);
            PurchaseInvoiceRepository = new PurchaseInvoiceRepository(context);
        }

        public DbConnection DbConnection()
        {
            return _context.GetDbConnect();
        }

        public IReportRepository ReportRepository { get; set; }
        public IPurchaseInvoiceRepository PurchaseInvoiceRepository { get; set;}
    }
}
