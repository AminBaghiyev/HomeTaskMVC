using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mediplus.DAL.Migrations
{
    /// <inheritdoc />
    public partial class IsActiveAddedToSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Settings");
        }
    }
}
