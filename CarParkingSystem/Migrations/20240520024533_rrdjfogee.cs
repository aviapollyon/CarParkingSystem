using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingSystem.Migrations
{
    public partial class rrdjfogee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FaultReport",
                table: "FaultReport");

            migrationBuilder.RenameTable(
                name: "FaultReport",
                newName: "FaultReports");

            migrationBuilder.AddColumn<long>(
                name: "AssignedTech",
                table: "FaultReports",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FaultReports",
                table: "FaultReports",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FaultReports",
                table: "FaultReports");

            migrationBuilder.DropColumn(
                name: "AssignedTech",
                table: "FaultReports");

            migrationBuilder.RenameTable(
                name: "FaultReports",
                newName: "FaultReport");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FaultReport",
                table: "FaultReport",
                column: "Id");
        }
    }
}
