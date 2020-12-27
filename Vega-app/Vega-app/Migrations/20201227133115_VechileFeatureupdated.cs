using Microsoft.EntityFrameworkCore.Migrations;

namespace Vega_app.Migrations
{
    public partial class VechileFeatureupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VechileFeature");

            migrationBuilder.CreateTable(
                name: "VechileFeatures",
                columns: table => new
                {
                    VehicleId = table.Column<int>(nullable: false),
                    FeatureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VechileFeatures", x => new { x.VehicleId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_VechileFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VechileFeatures_vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VechileFeatures_FeatureId",
                table: "VechileFeatures",
                column: "FeatureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VechileFeatures");

            migrationBuilder.CreateTable(
                name: "VechileFeature",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VechileFeature", x => new { x.VehicleId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_VechileFeature_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VechileFeature_vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VechileFeature_FeatureId",
                table: "VechileFeature",
                column: "FeatureId");
        }
    }
}
