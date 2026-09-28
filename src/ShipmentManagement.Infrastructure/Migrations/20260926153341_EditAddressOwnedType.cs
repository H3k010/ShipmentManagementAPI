using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditAddressOwnedType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestinationAddress_Country",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "OriginAddress_Country",
                table: "Packages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DestinationAddress_Country",
                table: "Packages",
                type: "VARCHAR(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OriginAddress_Country",
                table: "Packages",
                type: "VARCHAR(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
