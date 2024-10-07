using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eStore.Migrations
{
    public partial class FixOrderTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OrderAt",
                table: "OrderTable",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderAt",
                table: "OrderTable");
        }
    }
}
