using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SiriusStyleRd.Entities.Migrations
{
    public partial class additionalearningsanddate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AdditionalEarnings",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaidOrShippedOn",
                table: "Order",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalEarnings",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "PaidOrShippedOn",
                table: "Order");
        }
    }
}
