using InventoryManagement.Commons.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Domains.Entities
{
    [Table("MerchandiseSaleInvoices")]
    public class MerchandiseSaleInvoice
    {
        public Guid SaleInvoiceId { get; set; }
        public SaleInvoice SaleInvoice { get; set; }

        public Guid MerchandiseId { get; set; }
        public Merchandise Merchandise { get; set; }

        public float MerchandisePrice { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public float SellingPrice { get; set; }
        public float Voucher { get; set; }

        [DefaultValue(ActiveEnum.Active)]
        public ActiveEnum IsActive { get; set; }
    }
}
