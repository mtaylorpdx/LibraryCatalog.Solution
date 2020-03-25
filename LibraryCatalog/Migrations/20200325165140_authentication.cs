using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryCatalog.Migrations
{
    public partial class authentication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Patrons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Checkout",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Book",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Authors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patrons_UserId",
                table: "Patrons",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkout_UserId",
                table: "Checkout",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_UserId",
                table: "Book",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_UserId",
                table: "Authors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_AspNetUsers_UserId",
                table: "Authors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_AspNetUsers_UserId",
                table: "Book",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Checkout_AspNetUsers_UserId",
                table: "Checkout",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patrons_AspNetUsers_UserId",
                table: "Patrons",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_AspNetUsers_UserId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_AspNetUsers_UserId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Checkout_AspNetUsers_UserId",
                table: "Checkout");

            migrationBuilder.DropForeignKey(
                name: "FK_Patrons_AspNetUsers_UserId",
                table: "Patrons");

            migrationBuilder.DropIndex(
                name: "IX_Patrons_UserId",
                table: "Patrons");

            migrationBuilder.DropIndex(
                name: "IX_Checkout_UserId",
                table: "Checkout");

            migrationBuilder.DropIndex(
                name: "IX_Book_UserId",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Authors_UserId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Patrons");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Checkout");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Authors");
        }
    }
}
