using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryCatalog.Migrations
{
    public partial class Fifth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    CopyName = table.Column<string>(nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
