using Microsoft.EntityFrameworkCore.Migrations;

namespace SiriusStyleRdStore.Entities.Migrations
{
    public partial class Bale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BaleId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bale",
                columns: table => new
                {
                    BaleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 300, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    BoughtTo = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bale", x => x.BaleId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_BaleId",
                table: "Product",
                column: "BaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Bale_BaleId",
                table: "Product",
                column: "BaleId",
                principalTable: "Bale",
                principalColumn: "BaleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Bale_BaleId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Bale");

            migrationBuilder.DropIndex(
                name: "IX_Product_BaleId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "BaleId",
                table: "Product");
        }
    }
}
