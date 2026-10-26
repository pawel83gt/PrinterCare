using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrinterCare.Server.Migrations
{
    /// <inheritdoc />
    public partial class Cartridge040826 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_Cartridge_CartridgeId",
                table: "Equipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cartridge",
                table: "Cartridge");

            migrationBuilder.RenameTable(
                name: "Cartridge",
                newName: "Cartridges");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cartridges",
                table: "Cartridges",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_Cartridges_CartridgeId",
                table: "Equipments",
                column: "CartridgeId",
                principalTable: "Cartridges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_Cartridges_CartridgeId",
                table: "Equipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cartridges",
                table: "Cartridges");

            migrationBuilder.RenameTable(
                name: "Cartridges",
                newName: "Cartridge");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cartridge",
                table: "Cartridge",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_Cartridge_CartridgeId",
                table: "Equipments",
                column: "CartridgeId",
                principalTable: "Cartridge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
