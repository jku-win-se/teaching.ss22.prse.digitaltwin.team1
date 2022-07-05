using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace SmartRoom.TransDataService.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class splittetStates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "State");

            migrationBuilder.DropColumn(
                name: "BinaryValue",
                table: "State");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "State");

            migrationBuilder.DropColumn(
                name: "MeasureValue",
                table: "State");

            migrationBuilder.RenameTable(
                name: "State",
                newName: "MeasureStates");

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "MeasureStates",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "BinaryStates",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    EntityRefID = table.Column<Guid>(type: "uuid", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Value = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.Sql("SELECT create_hypertable('public.\"BinaryStates\"', 'TimeStamp');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BinaryStates");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "MeasureStates");

            migrationBuilder.RenameTable(
                name: "MeasureStates",
                newName: "State");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "State",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "BinaryValue",
                table: "State",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "State",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "MeasureValue",
                table: "State",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_State",
                table: "State",
                column: "Id");
        }
    }
}
