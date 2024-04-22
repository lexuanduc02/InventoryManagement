using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;

namespace InventoryManagement.Models.ReportModels
{
    public class PurchaseReportViewModel
    {
        public string? ReportAuthor { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public float TotalAmount { get; set; }
        public List<ProductExportImportDetail> Details { get; set; }
    }
}
