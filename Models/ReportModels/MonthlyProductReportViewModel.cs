namespace InventoryManagement.Models.ReportModels
{
    public class MonthlyProductReportViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
        public float? AmountPurchaseCurrentMonth { get; set; }
        public int? QuantityImportCurrentMonth { get; set; }
        public float? AmountPurchaseLastMonth { get; set;}
        public int? QuantityImportLastMonth { get; set; }

        public float AmountSaleCurrentMonth { get; set; }
        public int? QuantitySoldCurrentMonth { get; set; }
        public float AmountSaleLastMonth { get; set;}
        public int? QuantitySoldLastMonth { get; set; }

        public float? DifferenceImport
        {
            get
            {
                if (AmountPurchaseCurrentMonth == 0)
                    return 0;
                else if (AmountPurchaseLastMonth == 0)
                    return 100;
                else
                {
                    var deffrence = ((AmountPurchaseCurrentMonth - AmountPurchaseLastMonth) / AmountPurchaseLastMonth) * 100;
                    return (float?)Math.Round((decimal)deffrence, 2);
                }    
            }
        }

        public float? DifferenceSale
        {
            get
            {
                if (AmountSaleCurrentMonth == 0)
                    return 0;
                else if (AmountSaleLastMonth == 0)
                    return 100;
                else
                    return ((AmountSaleCurrentMonth - AmountSaleLastMonth) / AmountSaleLastMonth) * 100;
            }
        }
    }
}
