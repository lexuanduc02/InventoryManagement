using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Domains.Migrations
{
    /// <inheritdoc />
    public partial class alter_table_user_add_change_password_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChangePassword",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangePassword",
                table: "Users");
        }
    }
}
