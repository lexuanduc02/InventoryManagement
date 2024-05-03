using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Domains.Migrations
{
    /// <inheritdoc />
    public partial class alter_table_warehoses_add_warehouse_capacity_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarehouseCapacity",
                table: "Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WarehouseCapacity",
                table: "Warehouses");
        }
    }
}
