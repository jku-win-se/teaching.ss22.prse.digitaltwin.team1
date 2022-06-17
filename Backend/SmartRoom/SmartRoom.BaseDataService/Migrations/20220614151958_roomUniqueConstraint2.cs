using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace SmartRoom.BaseDataService.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class roomUniqueConstraint2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rooms_Name",
                table: "Rooms");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Name_Building",
                table: "Rooms",
                columns: new[] { "Name", "Building" },
                unique: true);
        }
        [ExcludeFromCodeCoverage]
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rooms_Name_Building",
                table: "Rooms");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Name",
                table: "Rooms",
                column: "Name",
                unique: true);
        }
    }
}
