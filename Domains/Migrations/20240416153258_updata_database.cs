using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Domains.Migrations
{
    /// <inheritdoc />
    public partial class updata_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvoiceTypeEnum",
                table: "PurchaseInvoices",
                newName: "InvoiceType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvoiceType",
                table: "PurchaseInvoices",
                newName: "InvoiceTypeEnum");
        }
    }
}
