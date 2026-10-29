using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrinterCare.Server.Migrations
{
    /// <inheritdoc />
    public partial class BranchRestrictDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Organizations_OrganizationId",
                table: "Branches");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Organizations_OrganizationId",
                table: "Branches",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Organizations_OrganizationId",
                table: "Branches");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Organizations_OrganizationId",
                table: "Branches",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
