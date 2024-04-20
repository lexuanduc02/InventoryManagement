using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Domains.Migrations
{
    /// <inheritdoc />
    public partial class alter_table_warehouse_remove_storage_capacity_and_unit_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StorageCapacity",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Warehouses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "StorageCapacity",
                table: "Warehouses",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
