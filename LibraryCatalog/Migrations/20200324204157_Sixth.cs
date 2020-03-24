using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryCatalog.Migrations
{
    public partial class Sixth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CopyName",
                table: "Copy");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Copy",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Copy");

            migrationBuilder.AddColumn<string>(
                name: "CopyName",
                table: "Copy",
                nullable: true);
        }
    }
}
