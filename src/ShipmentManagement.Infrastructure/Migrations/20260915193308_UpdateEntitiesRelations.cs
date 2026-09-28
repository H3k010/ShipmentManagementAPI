using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntitiesRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackingEvents_Facilities_FacilityId",
                table: "TrackingEvents");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackingEvents_Facilities_FacilityId",
                table: "TrackingEvents",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackingEvents_Facilities_FacilityId",
                table: "TrackingEvents");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackingEvents_Facilities_FacilityId",
                table: "TrackingEvents",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
