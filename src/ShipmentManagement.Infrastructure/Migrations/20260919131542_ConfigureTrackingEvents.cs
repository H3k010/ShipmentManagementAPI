using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureTrackingEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacilityName",
                table: "TrackingEvents",
                type: "VARCHAR(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackageName",
                table: "TrackingEvents",
                type: "VARCHAR(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacilityName",
                table: "TrackingEvents");

            migrationBuilder.DropColumn(
                name: "PackageName",
                table: "TrackingEvents");
        }
    }
}
