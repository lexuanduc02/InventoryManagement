using InventoryManagement.Commons.Enums;
using InventoryManagement.Models.CustomerModels;
using System.ComponentModel;

namespace InventoryManagement.Models.SaleInvoiceModels
{
    public class CreateSaleReturnInvoiceRequest
    {
        public string UserId { get; set; }
        public string? CustomerId { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public InvoiceStatusEnum Status { get; set; } = InvoiceStatusEnum.InProcess;
        public string? Note { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public ActiveEnum IsActive { get; set; } = ActiveEnum.Active;
        public List<MerchandiseSaleReturn> MerchandiseSaleInvoices { get; set; }
    }

    public class MerchandiseSaleReturn
    {
        public Guid? SaleInvoiceId { get; set; }
        public Guid MerchandiseId { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        [DefaultValue(ActiveEnum.Active)]
        public ActiveEnum IsActive { get; set; } = ActiveEnum.Active;
    }
}
