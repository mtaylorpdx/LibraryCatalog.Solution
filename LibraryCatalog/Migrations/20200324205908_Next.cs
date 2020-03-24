using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryCatalog.Migrations
{
    public partial class Next : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Copy_CopyId",
                table: "Book");

            migrationBuilder.DropTable(
                name: "CopyTitle");

            migrationBuilder.DropTable(
                name: "Copy");

            migrationBuilder.DropIndex(
                name: "IX_Book_CopyId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "CopyId",
                table: "Book");

            migrationBuilder.AddColumn<int>(
                name: "PatronId",
                table: "Titles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Titles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Patrons",
                columns: table => new
                {
                    PatronId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PatronName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patrons", x => x.PatronId);
                });

            migrationBuilder.CreateTable(
                name: "Checkout",
                columns: table => new
                {
                    CheckoutId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TitleId = table.Column<int>(nullable: false),
                    PatronId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkout", x => x.CheckoutId);
                    table.ForeignKey(
                        name: "FK_Checkout_Patrons_PatronId",
                        column: x => x.PatronId,
                        principalTable: "Patrons",
                        principalColumn: "PatronId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Checkout_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "TitleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Titles_PatronId",
                table: "Titles",
                column: "PatronId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkout_PatronId",
                table: "Checkout",
                column: "PatronId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkout_TitleId",
                table: "Checkout",
                column: "TitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Titles_Patrons_PatronId",
                table: "Titles",
                column: "PatronId",
                principalTable: "Patrons",
                principalColumn: "PatronId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Titles_Patrons_PatronId",
                table: "Titles");

            migrationBuilder.DropTable(
                name: "Checkout");

            migrationBuilder.DropTable(
                name: "Patrons");

            migrationBuilder.DropIndex(
                name: "IX_Titles_PatronId",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "PatronId",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Titles");

            migrationBuilder.AddColumn<int>(
                name: "CopyId",
                table: "Book",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Copy",
                columns: table => new
                {
                    CopyId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Copy", x => x.CopyId);
                });

            migrationBuilder.CreateTable(
                name: "CopyTitle",
                columns: table => new
                {
                    CopyTitleId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CopyId = table.Column<int>(nullable: false),
                    TitleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CopyTitle", x => x.CopyTitleId);
                    table.ForeignKey(
                        name: "FK_CopyTitle_Copy_CopyId",
                        column: x => x.CopyId,
                        principalTable: "Copy",
                        principalColumn: "CopyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CopyTitle_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "TitleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_CopyId",
                table: "Book",
                column: "CopyId");

            migrationBuilder.CreateIndex(
                name: "IX_CopyTitle_CopyId",
                table: "CopyTitle",
                column: "CopyId");

            migrationBuilder.CreateIndex(
                name: "IX_CopyTitle_TitleId",
                table: "CopyTitle",
                column: "TitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Copy_CopyId",
                table: "Book",
                column: "CopyId",
                principalTable: "Copy",
                principalColumn: "CopyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
