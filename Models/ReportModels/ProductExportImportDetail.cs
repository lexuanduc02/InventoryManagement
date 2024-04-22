using InventoryManagement.Commons.Enums;

namespace InventoryManagement.Models.ReportModels
{
    public class ProductExportImportDetail
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public float Total { get; set; }
        public InvoiceTypeEnum InvoiceType { get; set; }
    }
}
