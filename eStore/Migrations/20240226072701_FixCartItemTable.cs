using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eStore.Migrations
{
    public partial class FixCartItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quality",
                table: "CartItem",
                newName: "Quantity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "CartItem",
                newName: "Quality");
        }
    }
}
