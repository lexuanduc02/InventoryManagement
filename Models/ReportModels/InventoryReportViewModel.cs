using InventoryManagement.Models.MerchandiseModels;

namespace InventoryManagement.Models.ReportModels
{
    public class InventoryReportViewModel
    {
        public string WarehouseName { get; set; }
        public List<ProductViewModel> products { get; set; }
    }
}
