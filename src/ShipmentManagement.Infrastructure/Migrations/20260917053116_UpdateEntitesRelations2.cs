using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntitesRelations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackingEvents_Packages_PackageId",
                table: "TrackingEvents");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackingEvents_Packages_PackageId",
                table: "TrackingEvents",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackingEvents_Packages_PackageId",
                table: "TrackingEvents");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackingEvents_Packages_PackageId",
                table: "TrackingEvents",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
