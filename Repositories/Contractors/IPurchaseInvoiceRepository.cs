using InventoryManagement.Domains.EF;
using InventoryManagement.Domains.Entities;

namespace InventoryManagement.Repositories.Contractors
{
    public interface IPurchaseInvoiceRepository : IRepository<PurchaseInvoice, Guid, DataContext>
    {
    }
}
