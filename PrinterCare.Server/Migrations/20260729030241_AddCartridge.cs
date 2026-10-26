using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrinterCare.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddCartridge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CartridgeId",
                table: "Equipments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Cartridge",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartridge", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_CartridgeId",
                table: "Equipments",
                column: "CartridgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_Cartridge_CartridgeId",
                table: "Equipments",
                column: "CartridgeId",
                principalTable: "Cartridge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_Cartridge_CartridgeId",
                table: "Equipments");

            migrationBuilder.DropTable(
                name: "Cartridge");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_CartridgeId",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "CartridgeId",
                table: "Equipments");
        }
    }
}
