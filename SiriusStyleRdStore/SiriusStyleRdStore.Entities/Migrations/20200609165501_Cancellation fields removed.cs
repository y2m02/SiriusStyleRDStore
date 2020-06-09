using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SiriusStyleRdStore.Entities.Migrations
{
    public partial class Cancellationfieldsremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WasCanceled",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CanceledOn",
                table: "Order");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WasCanceled",
                table: "Product",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CanceledOn",
                table: "Order",
                type: "datetime2",
                nullable: true);
        }
    }
}
