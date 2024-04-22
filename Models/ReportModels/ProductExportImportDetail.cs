using InventoryManagement.Commons.Enums;
using InventoryManagement.Commons.Extensions;

namespace InventoryManagement.Models.ReportModels
{
    public class ProductExportImportDetail
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public float Total { get; set; }
        public float OriginalTotal { get; set; }
        public InvoiceTypeEnum InvoiceType { get; set; }

        public string TotalToString 
        {
            get {
                return Total.FormatVietnameseCurrency();
            }
        }

        public string OriginalTotalToString
        {
            get
            {
                return OriginalTotal.FormatVietnameseCurrency();
            }
        }
    }
}
