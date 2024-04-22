using Dapper;
using InventoryManagement.Commons.Enums;
using InventoryManagement.Domains.EF;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Models.ReportModels;
using InventoryManagement.Repositories.Contractors;

namespace InventoryManagement.Repositories
{
    public class ReportRepository : Repository<Merchandise, Guid, DataContext>, IReportRepository
    {
        public ReportRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<MonthlyProductReportViewModel>> MonthlyProductReport(DateTime date)
        {
            var month = date.Month;
            var year = date.Year;
            var invoiceType = (int)InvoiceTypeEnum.Invoice;

            var queryString =
                $"""
                    SELECT
                	    p1.id,
                	    p1.name,
                        p1.quantity,
                        p1.unit,
                	    isnull (p1.tsa1, 0) AS 'amountPurchaseCurrentMonth',
                	    isnull (p1.pquan1, 0) AS 'quantityImportCurrentMonth',
                	    isnull (p2.tsa2, 0) AS 'amountPurchaseLastMonth',
                	    isnull (p2.pquan2, 0) AS 'quantityImportLastMonth',
                	    isnull (s1.lmr1, 0) AS 'amountSaleCurrentMonth',
                	    isnull (s1.squan1, 0) AS 'quantitySoldCurrentMonth',
                	    isnull (s2.lmr2, 0) AS 'amountSaleLastMonth',
                	    isnull (s2.squan2, 0) AS 'quantitySoldLastMonth'
                    FROM
                	    (
                		    SELECT
                			    merchandises.id,
                			    merchandises.name,
                                merchandises.quantity,
                                merchandises.unit,
                			    SUM(
                				    merchandisepurchaseinvoices.quantity*merchandisepurchaseinvoices.purchaseprice
                			    ) AS tsa1,
                			    SUM(merchandisepurchaseinvoices.quantity) AS pquan1
                		    FROM
                			    merchandises
                			    LEFT JOIN merchandisepurchaseinvoices ON merchandises.id=merchandisepurchaseinvoices.merchandiseid
                			    LEFT JOIN purchaseinvoices ON merchandisepurchaseinvoices.purchaseinvoiceid=purchaseinvoices.id
                		    WHERE
                			    purchaseinvoices.invoicetype={invoiceType}
                			    AND MONTH (createat)={month}
                                AND YEAR (createat)={year}
                			    OR purchaseinvoices.id IS NULL
                		    GROUP BY
                			    merchandises.id,
                			    merchandises.name,
                			    merchandises.quantity,
                                merchandises.unit
                	    ) AS p1
                	    LEFT JOIN (
                		    SELECT
                			    merchandises.id,
                			    SUM(
                				    merchandisepurchaseinvoices.quantity*merchandisepurchaseinvoices.purchaseprice
                			    ) AS tsa2,
                			    SUM(merchandisepurchaseinvoices.quantity) AS pquan2
                		    FROM
                			    merchandises
                			    LEFT JOIN merchandisepurchaseinvoices ON merchandises.id=merchandisepurchaseinvoices.merchandiseid
                			    LEFT JOIN purchaseinvoices ON merchandisepurchaseinvoices.purchaseinvoiceid=purchaseinvoices.id
                		    WHERE
                			    purchaseinvoices.invoicetype={invoiceType}
                			    AND MONTH (createat)={month - 1}
                                AND YEAR (createat)={year}
                			    OR purchaseinvoices.id IS NULL
                		    GROUP BY
                			    merchandises.id
                	    ) AS p2 ON p1.id=p2.id
                	    LEFT JOIN (
                		    SELECT
                			    merchandises.id,
                			    SUM(
                				    merchandisesaleinvoices.quantity*merchandisesaleinvoices.sellingprice
                			    ) AS lmr1,
                			    SUM(merchandisesaleinvoices.quantity) AS squan1
                		    FROM
                			    merchandises
                			    LEFT JOIN merchandisesaleinvoices ON merchandises.id=merchandisesaleinvoices.merchandiseid
                			    LEFT JOIN saleinvoices ON merchandisesaleinvoices.saleinvoiceid=saleinvoices.id
                		    WHERE
                			    saleinvoices.invoicetype={invoiceType}
                			    AND MONTH (createat)={month}
                                AND YEAR (createat)={year}
                			    OR saleinvoices.id IS NULL
                		    GROUP BY
                			    merchandises.id
                	    ) AS s1 ON p1.id=s1.id
                	    LEFT JOIN (
                		    SELECT
                			    merchandises.id,
                			    SUM(
                				    merchandisesaleinvoices.quantity*merchandisesaleinvoices.sellingprice
                			    ) AS lmr2,
                			    SUM(merchandisesaleinvoices.quantity) AS squan2
                		    FROM
                			    merchandises
                			    LEFT JOIN merchandisesaleinvoices ON merchandises.id=merchandisesaleinvoices.merchandiseid
                			    LEFT JOIN saleinvoices ON merchandisesaleinvoices.saleinvoiceid=saleinvoices.id
                		    WHERE
                			    saleinvoices.invoicetype={invoiceType}
                			    AND MONTH (createat)={month - 1}
                                AND YEAR (createat)={year}
                			    OR saleinvoices.id IS NULL
                		    GROUP BY
                			    merchandises.id
                	    ) AS s2 ON s1.id=s2.id
                """
            ;

            var data = await _context.GetDbConnect().QueryAsync<MonthlyProductReportViewModel>(queryString);
                
            return data.ToList();
        }

        public async Task<List<ProductExportImportDetail>> PurchaseReport(DateTime? startDate, DateTime? endDate)
        {
            var queryString =
                $"""
                    SELECT Merchandises.Id, Merchandises.Name, Merchandises.Unit, PurchaseInvoices.InvoiceType,
                	    SUM(MerchandisePurchaseInvoices.Quantity) AS Quantity,
                	    SUM(MerchandisePurchaseInvoices.Quantity * MerchandisePurchaseInvoices.PurchasePrice) AS Total
                    FROM
                	    Merchandises
                	    LEFT JOIN dbo.MerchandisePurchaseInvoices ON Merchandises.Id = MerchandisePurchaseInvoices.MerchandiseId
                	    LEFT JOIN dbo.PurchaseInvoices ON MerchandisePurchaseInvoices.PurchaseInvoiceId = PurchaseInvoices.Id
                    WHERE PurchaseInvoices.CreateAt >= '{endDate}'
                		AND PurchaseInvoices.CreateAt <= '{startDate}'
                    GROUP BY Merchandises.Id, Merchandises.Name, Merchandises.Unit, PurchaseInvoices.InvoiceType;
                """;

            var data = await _context.GetDbConnect().QueryAsync<ProductExportImportDetail>(queryString);

            return data.ToList();
        }
    }
}
