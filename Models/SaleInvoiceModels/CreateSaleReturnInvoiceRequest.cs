using InventoryManagement.Commons.Enums;

namespace InventoryManagement.Models.SaleInvoiceModels
{
    public class CreateSaleReturnInvoiceRequest
    {
        public string UserId { get; set; }
        public string PartnerId { get; set; }
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
        public float SellingPrice { get; set; }
        public ActiveEnum IsActive { get; set; } = ActiveEnum.Active;
    }
}
