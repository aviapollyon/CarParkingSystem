using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingSystem.Migrations
{
    public partial class _2r429tijgk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FaultReports",
                table: "FaultReports");

            migrationBuilder.RenameTable(
                name: "FaultReports",
                newName: "FaultReport");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FaultReport",
                table: "FaultReport",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FaultReport",
                table: "FaultReport");

            migrationBuilder.RenameTable(
                name: "FaultReport",
                newName: "FaultReports");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FaultReports",
                table: "FaultReports",
                column: "Id");
        }
    }
}
