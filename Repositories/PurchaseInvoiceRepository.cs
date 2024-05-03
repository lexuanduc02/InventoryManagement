using InventoryManagement.Domains.EF;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Models.MerchandisePurchaseModels;
using InventoryManagement.Repositories.Contractors;

namespace InventoryManagement.Repositories
{
    public class PurchaseInvoiceRepository : Repository<PurchaseInvoice, Guid, DataContext>, IPurchaseInvoiceRepository
    {
        public PurchaseInvoiceRepository(DataContext context) : base(context)
        {
        }
    }
}
