using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eStore.Migrations
{
    public partial class AddRatingAverageForProductClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ratingAverage",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ratingAverage",
                table: "Product");
        }
    }
}
