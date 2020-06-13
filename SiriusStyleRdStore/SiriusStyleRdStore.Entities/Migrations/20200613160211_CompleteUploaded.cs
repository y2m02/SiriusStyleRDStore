using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SiriusStyleRdStore.Entities.Migrations
{
    public partial class CompleteUploaded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CompleteUploaded",
                table: "Bale",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Bale",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompleteUploaded",
                table: "Bale");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Bale");
        }
    }
}
