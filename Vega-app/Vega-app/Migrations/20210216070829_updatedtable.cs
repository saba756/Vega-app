using Microsoft.EntityFrameworkCore.Migrations;

namespace Vega_app.Migrations
{
    public partial class updatedtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_vehicles_VehicleId",
                table: "Photos");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Photos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_vehicles_VehicleId",
                table: "Photos",
                column: "VehicleId",
                principalTable: "vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_vehicles_VehicleId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Photos");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Photos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_vehicles_VehicleId",
                table: "Photos",
                column: "VehicleId",
                principalTable: "vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
