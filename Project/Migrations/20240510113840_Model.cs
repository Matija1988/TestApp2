using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectService.Migrations
{
    /// <inheritdoc />
    public partial class Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModel_VehicleMakers_MakeId",
                table: "VehicleModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleModel",
                table: "VehicleModel");

            migrationBuilder.RenameTable(
                name: "VehicleModel",
                newName: "VehicleModels");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleModel_MakeId",
                table: "VehicleModels",
                newName: "IX_VehicleModels_MakeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleModels",
                table: "VehicleModels",
                column: "Id");

            migrationBuilder.InsertData(
                table: "VehicleModels",
                columns: new[] { "Id", "Abrv", "MakeId", "Name" },
                values: new object[] { 1, "M3", 1, "Motorsport 3" });

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModels_VehicleMakers_MakeId",
                table: "VehicleModels",
                column: "MakeId",
                principalTable: "VehicleMakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_VehicleMakers_MakeId",
                table: "VehicleModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleModels",
                table: "VehicleModels");

            migrationBuilder.DeleteData(
                table: "VehicleModels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "VehicleModels",
                newName: "VehicleModel");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleModels_MakeId",
                table: "VehicleModel",
                newName: "IX_VehicleModel_MakeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleModel",
                table: "VehicleModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModel_VehicleMakers_MakeId",
                table: "VehicleModel",
                column: "MakeId",
                principalTable: "VehicleMakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
