using InventoryManagement.Commons.Extensions;

namespace InventoryManagement.Models.ReportModels
{
    public class InvoiceReportViewModel
    {
        public string? ReportAuthor { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float TotalAmount { get; set; }
        public float TotalReturnAmount { get; set; }
        public float OriginalTotalAmount { get; set; }
        public List<ProductExportImportDetail> Details { get; set; }

        public string StartDateString
        {
            get
            {
                return StartDate.ToDateOnly();
            }
        }

        public string EndDateString
        {
            get
            {
                return EndDate.ToDateOnly();
            }
        }

        public string TotalAmountToVND
        {
            get
            {
                return TotalAmount.FormatVietnameseCurrency();
            }
        }

        public string TotalReturnAmountToVND
        {
            get
            {
                return TotalReturnAmount.FormatVietnameseCurrency();
            }
        }

        public string OriginalTotalAmountToVND
        {
            get
            {
                return OriginalTotalAmount.FormatVietnameseCurrency();
            }
        }

    }
}
