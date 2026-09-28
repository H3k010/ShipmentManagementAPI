using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAdditionalProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Facilities",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Facilities");
        }
    }
}
