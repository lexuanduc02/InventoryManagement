using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Domains.Migrations
{
    /// <inheritdoc />
    public partial class update_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseInvoices_Partners_PartnerId",
                table: "PurchaseInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleInvoices_Customers_CustomerId",
                table: "SaleInvoices");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "SaleInvoices",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "PartnerId",
                table: "SaleInvoices",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PartnerId",
                table: "PurchaseInvoices",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "PurchaseInvoices",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleInvoices_PartnerId",
                table: "SaleInvoices",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoices_CustomerId",
                table: "PurchaseInvoices",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseInvoices_Customers_CustomerId",
                table: "PurchaseInvoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseInvoices_Partners_PartnerId",
                table: "PurchaseInvoices",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleInvoices_Customers_CustomerId",
                table: "SaleInvoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleInvoices_Partners_PartnerId",
                table: "SaleInvoices",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseInvoices_Customers_CustomerId",
                table: "PurchaseInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseInvoices_Partners_PartnerId",
                table: "PurchaseInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleInvoices_Customers_CustomerId",
                table: "SaleInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleInvoices_Partners_PartnerId",
                table: "SaleInvoices");

            migrationBuilder.DropIndex(
                name: "IX_SaleInvoices_PartnerId",
                table: "SaleInvoices");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseInvoices_CustomerId",
                table: "PurchaseInvoices");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "SaleInvoices");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "PurchaseInvoices");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "SaleInvoices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PartnerId",
                table: "PurchaseInvoices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseInvoices_Partners_PartnerId",
                table: "PurchaseInvoices",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleInvoices_Customers_CustomerId",
                table: "SaleInvoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
