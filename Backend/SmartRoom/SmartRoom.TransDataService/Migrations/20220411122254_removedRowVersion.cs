using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartRoom.TransDataService.Migrations
{
    public partial class removedRowVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "State");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "State",
                type: "bytea",
                rowVersion: true,
                nullable: true);
        }
    }
}
