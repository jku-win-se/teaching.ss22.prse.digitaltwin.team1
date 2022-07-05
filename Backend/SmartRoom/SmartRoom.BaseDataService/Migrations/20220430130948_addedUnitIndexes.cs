using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace SmartRoom.BaseDataService.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class addedUnitIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Name",
                table: "Rooms",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomEquipments_Name_EquipmentRef",
                table: "RoomEquipments",
                columns: new[] { "Name", "EquipmentRef" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rooms_Name",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_RoomEquipments_Name_EquipmentRef",
                table: "RoomEquipments");
        }
    }
}
