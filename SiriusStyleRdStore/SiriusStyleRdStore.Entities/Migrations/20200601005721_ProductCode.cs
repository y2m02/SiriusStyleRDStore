using Microsoft.EntityFrameworkCore.Migrations;

namespace SiriusStyleRdStore.Entities.Migrations
{
    public partial class ProductCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLine_Product_ProductCode",
                table: "OrderLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "Product",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "ProductCode");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLine_Product_ProductCode",
                table: "OrderLine",
                column: "ProductCode",
                principalTable: "Product",
                principalColumn: "ProductCode",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLine_Product_ProductCode",
                table: "OrderLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "Product",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLine_Product_ProductCode",
                table: "OrderLine",
                column: "ProductCode",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
